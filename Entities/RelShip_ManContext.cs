using Microsoft.EntityFrameworkCore;

namespace RelationShipManager.Entities
{
    public class RelShip_ManContext : DbContext
    {
        public RelShip_ManContext()
        {
        }

        public RelShip_ManContext(DbContextOptions<RelShip_ManContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Commune> Commune { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<MyUser> MyUser { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Wilaya> Wilaya { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=RelShip_Man;User ID=ict_towers;Password=ict_towers");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tables Constraints
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.Property(e => e.IdCategory)
                    .HasColumnName("idCategory")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Category1)
                    .IsRequired()
                    .HasColumnName("Category")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Commune>(entity =>
            {
                entity.HasKey(e => e.IdCommune);

                entity.Property(e => e.IdCommune)
                    .HasColumnName("idCommune")
                    .ValueGeneratedNever();

                entity.Property(e => e.CodePostal).HasColumnName("code_postal");

                entity.Property(e => e.Commune1)
                    .IsRequired()
                    .HasColumnName("Commune")
                    .HasMaxLength(50);

                entity.Property(e => e.WilayaId).HasColumnName("wilaya_id");

                entity.HasOne(d => d.Wilaya)
                    .WithMany(p => p.Commune)
                    .HasForeignKey(d => d.WilayaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commune_Wilaya");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.IdContact);

                entity.Property(e => e.IdContact)
                    .HasColumnName("idContact")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ContactInfo)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ContactType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('P')");

                entity.Property(e => e.IdMyUser).HasColumnName("idMyUser");

                entity.Property(e => e.IsPrimary)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.HasOne(d => d.IdMyUserNavigation)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.IdMyUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_MyUser");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdMyUser);

                entity.Property(e => e.IdMyUser)
                    .HasColumnName("idMyUser")
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.IdPosition).HasColumnName("idPosition");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Salaire).HasColumnType("money");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMyUserNavigation)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.IdMyUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_MyUser");

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.IdPosition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Position1");
            });

            modelBuilder.Entity<MyUser>(entity =>
            {
                entity.HasKey(e => e.IdMyUser);

                entity.Property(e => e.IdMyUser)
                    .HasColumnName("idMyUser")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Company)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IdCommune).HasColumnName("idCommune");

                entity.Property(e => e.Lname)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rue)
                    .HasColumnName("rue")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('S')");

                entity.HasOne(d => d.IdCommuneNavigation)
                    .WithMany(p => p.MyUser)
                    .HasForeignKey(d => d.IdCommune)
                    .HasConstraintName("FK_MyUser_Commune");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => new {e.IdMyUser, e.Date, e.IdProduct});

                entity.Property(e => e.IdMyUser).HasColumnName("idMyUser");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('S')");

                entity.HasOne(d => d.IdMyUserNavigation)
                    .WithMany(p => p.Operation)
                    .HasForeignKey(d => d.IdMyUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operation_MyUser");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition);

                entity.Property(e => e.IdPosition)
                    .HasColumnName("idPosition")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Position1)
                    .IsRequired()
                    .HasColumnName("Position")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.Property(e => e.IdProduct)
                    .HasColumnName("idProduct")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Product1)
                    .IsRequired()
                    .HasColumnName("Product")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<Wilaya>(entity =>
            {
                entity.HasKey(e => e.IdWilaya);

                entity.Property(e => e.IdWilaya)
                    .HasColumnName("idWilaya")
                    .ValueGeneratedNever();

                entity.Property(e => e.Wilaya1)
                    .IsRequired()
                    .HasColumnName("Wilaya")
                    .HasMaxLength(50);
            });
        }
    }
}
