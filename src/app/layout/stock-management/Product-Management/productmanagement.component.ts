import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { Subscription } from 'rxjs';
import { CategoryService } from './_services/category.service';
import {GrowlService} from 'ngx-growl';
import { Category } from 'src/app/_models';

@Component({
    selector: 'app-productmanagement',
    templateUrl: './productmanagement.component.html',
    styleUrls: ['./productmanagement.component.scss'],
    animations: [routerTransition()],
    providers: [CategoryService]
})
export class ProductManagementComponent implements OnInit {
    categories;
    getAllSubscription: Subscription;

    constructor(
        private categoryService: CategoryService,
        private growlService: GrowlService
    ) {}

    ngOnInit() {
        this.getAllCategories();
    }

    getAllCategories() {
        if (this.getAllSubscription) {
            this.getAllSubscription.unsubscribe();
          }
          this.getAllSubscription = this.categoryService.getAll().subscribe((data) => {
            this.categories = data;
          });
    }

    addCategory(categoryname: string) {
        this.categoryService.add(categoryname).subscribe((data) => {
            this.getAllCategories();
            this.growlService.addSuccess(`Category "` + categoryname + `" added successfully.`);
        });

    }

    deleteCategory(category: any) {
        this.categoryService.delete(category.idCategory).subscribe((data) => {
            this.getAllCategories();
            this.growlService.addSuccess(`Category "` + category.category1 + `" deleted successfully.`);
        });
    }

    editCategory(category: any, cat: Category) {
        console.log('ani f edit category');
        cat.category1 = category;
        this.categoryService.update(cat).subscribe((data) => {
            this.getAllCategories();
            this.growlService.addSuccess(`Category edited successfully.`);
        });
    }

    addProduct(product: any) {

    }

    deleteProduct(product: any) {

    }

    editProduct(product: any) {

    }

    deleteDisabled() {
        this.growlService.addError(`Can't delete non-empty category.`);
    }
}
