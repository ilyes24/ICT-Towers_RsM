import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../../../../_models';

@Injectable()
export class CategoryService {

    constructor(private http: HttpClient) {}

    getAll() {
        return this.http.get<Category>(`http://localhost:5000/api/Category`);
    }

    getById(id: number) {
        return this.http.get(`http://localhost:5000/api/Category/` + id);
    }

    add(category: string) {
        return this.http.post<Category>('http://localhost:5000/api/Category', {Category: category});
    }

    update(category: Category) {
        return this.http.put(`http://localhost:5000/api/Category/` + category.idCategory, category);
    }

    delete(id: number) {
        return this.http.delete<any>(`http://localhost:5000/api/Category?id=` + id);
    }
}
