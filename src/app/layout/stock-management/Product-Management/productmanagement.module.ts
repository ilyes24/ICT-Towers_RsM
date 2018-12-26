import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductManagementComponent } from './productmanagement.component';
import { ProductManagementRoutingModule } from './productmanagement-routing.module';
import { PageHeaderModule } from '../../../shared';
import { HttpClientModule } from '@angular/common/http';
import { AddCategoryComponent } from './components/addcategory/addcategory.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
    imports: [
        CommonModule,
        PageHeaderModule,
        HttpClientModule,
        NgbModule,
        ProductManagementRoutingModule,
        FormsModule,
        ReactiveFormsModule,
    ],
    declarations: [
        ProductManagementComponent,
        AddCategoryComponent
    ]
})
export class ProductManagementModule {}
