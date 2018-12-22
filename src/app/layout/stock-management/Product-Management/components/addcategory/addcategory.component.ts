import { Component, OnInit } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Category } from 'src/app/_models';
import { CategoryService } from '../../_services/category.service';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-addcategory',
    templateUrl: './addcategory.component.html',
    styleUrls: ['./addcategory.component.scss'],
    providers: [CategoryService]
})
export class AddCategoryComponent implements OnInit {
    categoryForm: FormGroup;
    closeResult: string;

    ngOnInit(): void {
        this.categoryForm = this.formBuilder.group({
            categoryName: ['', Validators.required],
        });
    }

    constructor(
        private http: HttpClient,
        private categoryService: CategoryService,
        private formBuilder: FormBuilder,
        private modalService: NgbModal
        ) { }

    get f() { return this.categoryForm.controls; }

    open(content) {
        this.modalService.open(content).result.then((result) => {
            this.closeResult = `Closed with: ${result}`;
        }, (reason) => {
            this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        });
    }

    addCategory() {
        if (this.categoryForm.invalid) {
            console.log('Form Invalid');
            return;
        }
        console.log('categoryname is:');
        console.log(this.f.categoryName.value);
        console.log('categoryname is:');
        this.http.post(`http://localhost:5000/api/Category`, this.f.categoryName.value);
    }

    private getDismissReason(reason: any): string {
        if (reason === ModalDismissReasons.ESC) {
            return 'by pressing ESC';
        } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
            return 'by clicking on a backdrop';
        } else {
            return  `with: ${reason}`;
        }
    }
}
