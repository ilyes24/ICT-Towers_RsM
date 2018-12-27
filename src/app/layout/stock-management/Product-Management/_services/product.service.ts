import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../../../../_models';

@Injectable()
export class ProductService {

    constructor(private http: HttpClient) {}

    getAll() {
        return this.http.get<Product>(`http://localhost:5000/api/Product`);
    }

    getById(id: number) {
        return this.http.get(`http://localhost:5000/api/Product/` + id);
    }

    add(product: Product) {
        return this.http.post<Product>('http://localhost:5000/api/Product', JSON.stringify(product));
    }

    update(product: Product) {
        console.log(product);
        return this.http.put(`http://localhost:5000/api/Product?id=` + product.IdProduct, JSON.stringify(product));
    }

    delete(id: number) {
        return this.http.delete<any>(`http://localhost:5000/api/Product?id=` + id);
    }
}
