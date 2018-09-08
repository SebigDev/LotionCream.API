import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-men',
  templateUrl: './men.component.html',
  encapsulation: ViewEncapsulation.None
})
export class MenComponent implements OnInit {

  categoryId: any;

  constructor(private _activatedRoute: ActivatedRoute,
              private _router: Router,
              ) { }

  ngOnInit() {
    this._activatedRoute.params.subscribe(params =>{
      this.categoryId = params.id;
    })
  }
  goToFair():void{
    this._activatedRoute.params.subscribe(params=>{
      this._router.navigate(['men-category/fair', params.id]);
    })
  }
}
