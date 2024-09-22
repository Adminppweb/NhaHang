/*global angular*/
(function () {
    angular
        .module('simplAdmin.catalog')
        .controller('CategoryFormCtrl', ['$q', '$state', '$stateParams', 'categoryService', 'translateService', CategoryFormCtrl]);

    function CategoryFormCtrl($q, $state, $stateParams, categoryService, translateService) {
        var vm = this,
            tableStateRef;
        vm.translate = translateService;
        vm.category = { description: '', isPublished: true };
        vm.categories = [];
        vm.products = [];
        vm.categoryId = $stateParams.id;
        vm.isEditMode = vm.categoryId > 0;
        vm.showProgress = false;

        vm.updateSlug = function () {
            vm.category.slug = slugify(vm.category.name);
        };

        vm.copySlug = function () {
            if (vm.category.slug) {
                var inputElement = document.createElement('input');
                inputElement.setAttribute('value', vm.category.slug);
                document.body.appendChild(inputElement);
                inputElement.select();
                document.execCommand('copy');
                document.body.removeChild(inputElement);

                toastr.success(translateService.get('Slug copied successfully'));
            }
        };

        // Tùy chọn của TinyMCE
        vm.tinymceMetaDescriptionOptions = {
            menubar: true,
            plugins: 'anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount code preview fullscreen',
            toolbar: 'undo redo blocks styleselect fontselect fontsizeselect | bold italic underline strikethrough | align lineheight checklist lists numlist bullist indent outdent | emoticons charmap removeformat fullscreen',
            max_height: 300,
            min_height: 150,
            max_width: 500,
            min_width: 400
        };

        vm.tinymceDescriptionOptions = {
            menubar: true,
            toolbar: [
                "undo redo cut copy paste blocks styleselect fontselect fontsizeselect bold italic underline strikethrough | align lineheight checklist bullist numlist outdent indent",
                "blockquote subscript superscript | advlist lists autolink table emoticons charmap | link image media | print preview code removeformat fullscreen"],
            plugins: 'anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount code preview fullscreen',
            max_height: 500,
            min_height: 800,
            max_width: 800,
            min_width: 400
        };
        vm.save = function save() {
            vm.showProgress = true;
            var promise;
            // ng-upload will post null as text
            vm.category.parentId = vm.category.parentId === null ? '' : vm.category.parentId;
            vm.category.metaTitle = vm.category.metaTitle === null ? '' : vm.category.metaTitle;
            vm.category.metaKeywords = vm.category.metaKeywords === null ? '' : vm.category.metaKeywords;
            vm.category.metaDescription = vm.category.metaDescription === null ? '' : vm.category.metaDescription;

            if (vm.isEditMode) {
                promise = categoryService.editCategory(vm.category);
            } else {
                promise = categoryService.createCategory(vm.category);
            }

            promise
                .then(function (response) {
                    vm.showProgress = false;
                    result = response.data;
                    if (result.statusCode == 200) {
                        toastr.success(translateService.get('Save change success'));
                        $state.go('category', { id: result.result });
                    }
                    else {
                        toastr.error(translateService.get('Excuse me. An error occurred during execution'));
                    }
                })
                .catch(function (response) {
                    vm.showProgress = false;
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push(translateService.get('Could not add category.'));
                    }
                });
        };

        vm.getProducts = function getProducts(tableState) {
            if (!vm.categoryId) {
                return;
            }

            tableStateRef = tableState;
            vm.isLoading = true;
            categoryService.getProducts(vm.categoryId, tableState).then(function (result) {
                vm.products = result.data.items;
                tableState.pagination.numberOfPages = result.data.numberOfPages;
                vm.isLoading = false;
            });
        };

        vm.editProduct = function editProduct(product) {
            product.isEditing = true;
            product.editingIsFeaturedProduct = product.isFeaturedProduct;
            product.editingDisplayOrder = product.displayOrder;
        };

        vm.saveProduct = function saveProduct(product) {
            var productCategory = {
                'id': product.id,
                'isFeaturedProduct': product.editingIsFeaturedProduct,
                'displayOrder': product.displayOrder
            };
            categoryService.saveProduct(productCategory).then(function () {
                product.isEditing = false;
                product.isFeaturedProduct = product.editingIsFeaturedProduct;
                product.displayOrder = product.editingDisplayOrder;
            });
        };

        function init() {
            if (vm.isEditMode) {
                $q.all([
                        categoryService.getCategories(),
                        categoryService.getCategory(vm.categoryId)
                    ])
                    .then(function (result) {
                        var index;
                        vm.categories = result[0].data;
                        vm.category = result[1].data;

                        index = vm.categories.map(function (item) {
                            return item.id;
                        }).indexOf(vm.category.id);
                        vm.categories.splice(index, 1);
                    });
            }
            else {
                categoryService.getCategories()
                    .then(function (result) {
                        vm.categories = result.data;
                });
            }
        }

        init();
    }
})();
