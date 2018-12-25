import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { Observable } from 'rxjs';
import { Category } from 'src/app/_models';
import { CategoryService } from './_services/category.service';

@Component({
    selector: 'app-productmanagement',
    templateUrl: './productmanagement.component.html',
    styleUrls: ['./productmanagement.component.scss'],
    animations: [routerTransition()],
    providers: [CategoryService]
})
export class ProductManagementComponent implements OnInit {
    categorysObservable: Observable<Category[]>;

    constructor(private categoryService: CategoryService) {}

    ngOnInit() {
        this.categorysObservable = this.categoryService.getAll();
        this.categorysObservable.forEach(category => {
            console.log(category);
        });
    }
}
