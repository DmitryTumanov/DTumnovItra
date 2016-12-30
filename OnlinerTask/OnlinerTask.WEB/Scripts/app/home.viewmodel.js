function SearchViewModel(app) {
    return this;
}

app.addViewModel({
    name: "Search",
    bindingMemberName: "search",
    factory: SearchViewModel
});
