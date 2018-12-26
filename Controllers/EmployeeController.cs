using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RelationShipManager.Dtos;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;
using RelationShipManager.Services;

namespace RelationShipManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeService employeeService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] EmployeeDto employeeDto)
        {
            var employee = _employeeService.Authenticate(employeeDto.UserName, employeeDto.Password);

            if (employee == null)
                return BadRequest(new
                {
                    message = employeeDto.UserName + "  " + employeeDto.Password + "  UserName or Password is incorrect"
                });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, employee.IdMyUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            employee.IdMyUserNavigation = new MyUserService().GetById(employee.IdMyUser);

            // return basic employee info (without password) and token to store client side
            return Ok(new
            {
                Id = employee.IdMyUser,
                employee.UserName,
                FirstName = employee.IdMyUserNavigation.Fname,
                LastName = employee.IdMyUserNavigation.Lname,
                Token = tokenString
            });
        }

        /// <summary>
        ///     Get idCommune and creating and setting a list of contacts,
        ///     Creating the user and setting the forieng key to employee,
        ///     Add idPosition to the employee.
        ///     Save the Employee
        /// </summary>
        /// <param name="employeeDto">
        ///     MyUser{
        ///     Fname, Lname, rue,
        ///     CommuneDto{
        ///     Commune
        ///     WilayaDto{
        ///     Wilaya
        ///     }
        ///     }
        ///     ContactsDtos:[ {ContactType, ContactInfo, IsPrimary} ... ]
        ///     }
        ///     UserName, Password, BirthDate, Salary,
        ///     PositionDto{
        ///     Posititon, BaseSalary
        ///     }
        /// </param>
        /// <returns>HTTP 200 OK(employee)</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] EmployeeDto employeeDto)
        {
            // map dto to entity
            var employee = _mapper.Map<Employee>(employeeDto);
            var user = _mapper.Map<MyUser>(employeeDto.myUser);
            try
            {
                IMyUserService _myUserService = new MyUserService();
                IPositionService _positionService = new PositionService();
                ICommuneService _communeService = new CommuneService();

                // Set idCommune for the user.
                user.IdCommune = _communeService.GetByName(employeeDto.myUser.communeDto.Commune).IdCommune;

                // Set the list of Contacts for the user.
                foreach (var contactDto in employeeDto.myUser.ContactsDtos)
                {
                    var contact = new Contact();
                    contact.ContactInfo = contactDto.ContactInfo;
                    contact.ContactType = contactDto.ContactType;
                    contact.IsPrimary = contactDto.IsPrimary.ToString();
                    user.Contact.Add(contact);
                }

                // Save the User.
                user = _myUserService.Create(user);

                // Set the idMyUser for the employee.
                employee.IdMyUser = user.IdMyUser;

                // Set the idPosition for the employee.
                employee.IdPosition = _positionService.GetByName(employeeDto.positionDto.Position).IdPosition;

                //Create and Save the employee
                var e = _employeeService.Create(employee, employeeDto.Password);

                // Return HTTP 200 OK requet with the employee JSON.  
                return Ok(e);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeService.GetAll();
            var employeeDtos = _mapper.Map<IList<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _employeeService.GetById(id);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeDto employeeDto)
        {
            // map dto to entity and set id
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.IdMyUser = id;

            try
            {
                // save
                _employeeService.Update(employee, employeeDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return Ok();
        }
    }
}
