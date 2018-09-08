import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Configuration } from './configuration';

import { CategoriesService } from './api/categories.service';
import { CommentsService } from './api/comments.service';
import { PostsService } from './api/posts.service';
import { ProductListsService } from './api/productLists.service';
import { ProductsService } from './api/products.service';
import { RepliesService } from './api/replies.service';
import { UsersService } from './api/users.service';

@NgModule({
  imports:      [ CommonModule, HttpClientModule ],
  declarations: [],
  exports:      [],
  providers: [
    CategoriesService,
    CommentsService,
    PostsService,
    ProductListsService,
    ProductsService,
    RepliesService,
    UsersService ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        }
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import your base AppModule only.');
        }
    }
}
