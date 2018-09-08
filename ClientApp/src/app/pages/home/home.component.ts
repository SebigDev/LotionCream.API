import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CategoryDto, CategoriesService } from '../../shared/client-services/typescript-angular-client';
import { Router, ActivatedRoute } from '../../../../node_modules/@angular/router';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent implements OnInit {
  categories: CategoryDto[];
  categoryId: any;
  category: CategoryDto = null;

  constructor(private _router: Router,
              private _activatedRoute: ActivatedRoute,
              private _categoryService: CategoriesService,
            ) { }

  ngOnInit() {
    this.getCategories();
  }

  getCategories(): void{
    this._categoryService.apiCategoriesGetAllCategoriesGet()
    .subscribe((categories: CategoryDto[]) =>{
      this.categories = categories;
    })
  }
  goToMen(id: number): void{
      this._router.navigate(['/men-category', id])
  }
}
