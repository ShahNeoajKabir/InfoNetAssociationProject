import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header-one',
  templateUrl: './header-one.component.html',
  styleUrls: ['./header-one.component.css']
})
export class HeaderOneComponent implements OnInit {
  openMenu:boolean=false;
  opens:number=1;
  compId:number=0;
  parentCategory:any[]=[];
  filterCategories:any[]=[];
  addition:any[]=[];
  products:any[]=[]
  categories:any[]=[];
  catagoriNames:any[]=[];
  subCategory:any[]=[];
  categoryId:Number=0;
  catagoriSub: any[] = [];
  blogNames: any[] = ['Romantic','Horror','Historical'];

  routeInfo={categoryId:0,isHeadinng:0};
  lstNavName:any[]=[
                  {"id":1,"name":" ","routerPath":""},
                  {"id":2,"name":"Catagories","routerPath": "/catagori-list"},
                  {"id":3,"name":"Book","routerPath":"/shop-list"},
                  {"id":4,"name":"Blog","routerPath":"/blog-list"},
                  {"id":5,"name":"About us","routerPath":"/about"},
                 ]

  constructor() { }

  ngOnInit(): void {
  }

}
