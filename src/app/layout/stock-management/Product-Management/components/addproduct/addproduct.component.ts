import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Product } from 'src/app/_models';

@Component({
    selector: 'app-addproduct',
    templateUrl: './addproduct.component.html',
    styleUrls: ['./addproduct.component.scss'],
})
export class AddProductComponent implements OnInit {
    @Output() productAdded: EventEmitter<Product> = new EventEmitter();

    productForm: FormGroup;
    closeResult: string;

    ngOnInit(): void {
        this.productForm = this.formBuilder.group({
            ProductName: ['', Validators.required],
            Reference: ['', Validators.required],
            SerialNumber: ['', Validators.required],
            Quantite: ['', Validators.required],
            Price: ['', Validators.required],
        });
    }

    constructor(
        private formBuilder: FormBuilder,
        private modalService: NgbModal
        ) { }

    get f() { return this.productForm.controls; }

    open(content) {
        this.modalService.open(content).result.then((result) => {
            this.closeResult = `Closed with: ${result}`;
        }, (reason) => {
            this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        });
    }

    addProduct() {
        if (this.productForm.invalid) {
            console.log('Form Invalid');
            return;
        }
        this.productAdded.emit(this.f.productName.value);
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
