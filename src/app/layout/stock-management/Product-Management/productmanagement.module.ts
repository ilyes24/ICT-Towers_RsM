import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductManagementComponent } from './productmanagement.component';
import { ProductManagementRoutingModule } from './productmanagement-routing.module';
import { PageHeaderModule } from '../../../shared';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AddCategoryComponent } from './components/addcategory/addcategory.component';
import { EditCategoryComponent } from './components/editcategory/editcategory.component';
import { AddProductComponent } from './components/addproduct/addproduct.component';
// import { EditProductComponent } from './components/editproduct/editproduct.component';

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
        AddCategoryComponent,
        EditCategoryComponent,
        AddProductComponent,
        // EditProductComponent
    ]
})
export class ProductManagementModule {}
