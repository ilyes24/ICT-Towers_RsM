import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';

@Component({
    selector: 'app-productmanagement',
    templateUrl: './productmanagement.component.html',
    styleUrls: ['./productmanagement.component.scss'],
    animations: [routerTransition()]
})
export class ProductManagementComponent implements OnInit {
    constructor() {}

    ngOnInit() {
    }
}
