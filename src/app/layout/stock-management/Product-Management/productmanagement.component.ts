import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { Subscription } from 'rxjs';
import { CategoryService } from './_services/category.service';

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

    constructor(private categoryService: CategoryService) {}

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

    categoryAdded(categoryname: string) {
        this.categoryService.add(categoryname).subscribe((data) => {
            this.getAllCategories();
        });

    }

    deleteCategory(category: any) {
        if (category.product.length > 0) {

        }
        this.categoryService.delete(category.idCategory).subscribe((data) => {
            this.getAllCategories();
        });
    }
}
