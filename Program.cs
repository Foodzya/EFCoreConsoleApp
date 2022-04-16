using EcommerceStore.Queries;

EagerLoadingQuery eagerLoading = new EagerLoadingQuery();
eagerLoading.CompletedOrdersWithProduct();
eagerLoading.BrandsWithProductsByDesc();
eagerLoading.ReviewsForProduct();
eagerLoading.ProductsByBrandName();
eagerLoading.ProductsBySectionAndCategory();

/*
LazyLoadingQuery lazyLoading = new LazyLoadingQuery();
lazyLoading.CompletedOrdersWithProduct();
lazyLoading.BrandsWithProductsByDesc();
lazyLoading.ReviewsForProduct();
lazyLoading.ProductsByBrandName();
lazyLoading.ProductsBySectionAndCategory();
*/

DisconnectedUpdate disconnectedUpdate = new DisconnectedUpdate();
disconnectedUpdate.UpdateUserExplicitTracking();
disconnectedUpdate.UpdateUserWithEntityState();
