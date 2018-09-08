declare var module: NodeModule;
declare var jquery: any;
interface NodeModule {
    id: string;
}
interface JQuery {
    mMenu(options: any): JQuery;
    animateClass(options: any): JQuery;
    setActiveItem(item: any): JQuery;
    getPageTitle(item: any): JQuery;
    getBreadcrumbs(item: any): JQuery;
    validate(options: any): JQuery;
    valid(): JQuery;
    resetForm(): JQuery;
    markdown(): JQuery;
    datepicker(item?: any): JQuery;
    date: Event;
    getDate(options: any): JQuery;
    google:any;
}
