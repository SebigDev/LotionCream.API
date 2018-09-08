import { Component, OnInit, Input } from '@angular/core';
import { CategoriesService, ProductsService, ProductDto, ProductListsService, ProductListDto } from '../../../shared/client-services/typescript-angular-client';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';

@Component({
  selector: 'app-fair',
  templateUrl: './fair.component.html',
  styleUrls: ['./fair.component.css']
})

  
export class FairComponent implements OnInit {
  products: ProductDto[];
  productLists: ProductListDto[];

  constructor(private _activatedRoute: ActivatedRoute,
              private _productService: ProductsService,
              private _productListService: ProductListsService) { }

  ngOnInit() {

   this._activatedRoute.params.subscribe(params =>{
      console.log(params.id);
      this.getProductsByCategory(params.id);
   })
  }

  getProductsByCategory(Id: number): void{
    this._productService.apiProductsGetProductByCategoryIDGet(Id)
    .subscribe((products: ProductDto[]) =>{
      this.products = products;
      for(let product of products)
      {
         this.getProductLists(product.productID);
      }
    })
  }

getProductLists(id: number): void{
  this._productListService.apiProductListsGetAllProductListsByProductIDGet(id)
  .subscribe((productLists: ProductListDto[]) =>{
    this.productLists = productLists;
    console.log(productLists);
  })
}

}
