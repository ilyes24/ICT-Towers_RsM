import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Category } from 'src/app/_models';

@Component({
    selector: 'app-addcategory',
    templateUrl: './addcategory.component.html',
    styleUrls: ['./addcategory.component.scss'],
})
export class AddCategoryComponent implements OnInit {
    @Output() categoryAdded: EventEmitter<Category> = new EventEmitter();

    categoryForm: FormGroup;
    closeResult: string;

    ngOnInit(): void {
        this.categoryForm = this.formBuilder.group({
            categoryName: ['', Validators.required],
        });
    }

    constructor(
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
        this.categoryAdded.emit(this.f.categoryName.value);
        this.modalService.dismissAll(ModalDismissReasons.ESC);
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
