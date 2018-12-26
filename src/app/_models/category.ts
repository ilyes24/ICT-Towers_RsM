import { Observable } from 'rxjs';
import { Product } from './product';

export class Category {

    idCategory: number;
    category1: string;
    product: Product[];

    constructor(
        idCategory: number,
        name: string,
        products: Product[]
    ) {
        this.idCategory = idCategory;
        this.category1 = name;
        this.product = products;
    }

    public printCategory() {
        console.log(this.idCategory + '   ' + this.category1);
    }
}
