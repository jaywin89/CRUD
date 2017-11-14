(function () {
    'use strict'
    console.log("here");

    angular.module(Reference).controller("referenceController", ReferenceController)

    ReferenceController.$inject = ["$scope", "referenceService", "$uibModal"];

    function ReferenceController($scope, referenceService, $uibModal) {
        var vm = this;
        vm.openExample = _openExample;
        vm.addRefModel = {};
        vm.searchRefModel = {};
        vm.submit = _submit;
        vm.getAllRefByType = _getAllByType;
        vm.getAllRefByTypePag = _getAllByTypePag;
        vm.allRefModel = [];
        vm.deleteRef = _deleteRef;
        vm.pagSettings = {
            pageNumber: 1,
            pageSize: 5,
            totalPages: 1,
            firstButton: 1,
            secondButton: 2,
            thirdButton: 3

        };
        vm.decrementPagNum = _decrementPagNum;
        vm.incrementPagNum = _incrementPagNum;
        vm.dynamicButton = _dynamicButton;
        vm.paginationFunction = _paginationFunction;
        vm.findWeather = _findWeather;
        vm.searchZipcode = "";
        vm.currentWeather = 0;

        function _findWeather() {
            referenceService.findWeather(vm.searchZipcode)
                .then(function (data) {
                    console.log(data);
                    vm.currentWeather = data.degrees;
                    vm.currentCity = data.address;

                }).catch(function (err) {
                    console.log(err);
                })
        }

        function _incrementPagNum() {
            vm.pagSettings.totalPages = vm.flaggedContents[0].totalPages;
            if (vm.pagSettings.pageNumber < vm.pagSettings.totalPages) {
                vm.pagSettings.pageNumber++;
                vm.searchRefModel.pageNumber = vm.pagSettings.pageNumber;
                vm.searchRefModel.pageSize = vm.pagSettings.pageSize;
                vm.getAllRefByTypePag(vm.searchRefModel);

                firstButton: 1,
                    _paginationFunction(vm.pagSettings.pageNumber, vm.pagSettings.totalPages);

            }

        }

        function _decrementPagNum() {

            if (vm.pagSettings.pageNumber > 1) {
                vm.pagSettings.pageNumber--;
                vm.searchRefModel.pageNumber = vm.pagSettings.pageNumber;
                vm.searchRefModel.pageSize = vm.pagSettings.pageSize;

                vm.getAllRefByTypePag(vm.searchRefModel);
                _paginationFunction(vm.pagSettings.pageNumber, vm.pagSettings.totalPages);

            }

        }

        function _paginationFunction(pageNumber, totalPages) {

            if (pageNumber <= 1) {
                vm.pagSettings.firstButton = pageNumber;
                vm.pagSettings.secondButton = pageNumber + 1;
                vm.pagSettings.thirdButton = pageNumber + 2;
            } else if (pageNumber === totalPages && pageNumber > 3) {
                vm.pagSettings.firstButton = pageNumber - 2;
                vm.pagSettings.secondButton = pageNumber - 1;
                vm.pagSettings.thirdButton = pageNumber;
            } else if (pageNumber > 1) {
                vm.pagSettings.firstButton = pageNumber - 1;
                vm.pagSettings.secondButton = pageNumber;
                vm.pagSettings.thirdButton = pageNumber + 1;
            }

        }
        function _dynamicButton(pageNumber) {
             vm.pagSettings.pageNumber = pageNumber;
            vm.searchRefModel.pageNumber = pageNumber;
            vm.searchRefModel.pageSize = vm.pagSettings.pageSize;
            vm.getAllRefByTypePag(vm.searchRefModel);

            _paginationFunction(pageNumber, vm.pagSettings.totalPages);
        }

        function _submit() {
  
            console.log(vm.searchRefModel);
            referenceService.addReference(vm.addRefModel)
                .then(function (data) {
                    //console.log(data);
            
                    //vm.addRefModel.categoryType.val() = 'Please Select';
                    vm.addRefModel = {};
                }).catch(function (err) {
                    console.log(err);
                })
        }

        function _getAllByType() {
            if (!vm.searchRefModel.categoryType) {
                vm.searchRefModel.categoryType = '';
            }
            vm.searchRefModel.pageNumber = 1;
            vm.searchRefModel.pageSize = 5;
            //console.log(vm.searchRefModel);
            referenceService.getAllRefByType(vm.searchRefModel)
                .then(function (data) {
 
                    vm.allRefModel = data;
                    //console.log(vm.allRefModel[0].totalPages)
                    vm.pagSettings.totalPages = vm.allRefModel[0].totalPages;
                    var pageNumber = 1;
                    vm.pagSettings.pageNumber = pageNumber;
                    _paginationFunction(pageNumber, vm.pagSettings.totalPages);
                    vm.pagSettings.firstButton = 1;
                    vm.pagSettings.secondButton = 2;
                    vm.pagSettings.thirdButton = 3;

                }).catch(function (err) {
                    console.log(err);
                })
        }
        function _getAllByTypePag() {
            if (!vm.searchRefModel.categoryType) {
                vm.searchRefModel.categoryType = '';
            }
            if (!vm.searchRefModel.pageNumber) {
                vm.searchRefModel.pageNumber = 1;
            }

            vm.searchRefModel.pageSize = 5;
            //console.log(vm.searchRefModel);
            referenceService.getAllRefByType(vm.searchRefModel)
                .then(function (data) {
       
                    vm.allRefModel = data;
                    //console.log(vm.allRefModel[1].totalPages)
                    vm.pagSettings.totalPages = vm.allRefModel[0].totalPages;
                }).catch(function (err) {
                    console.log(err);
                })
        }




        function _deleteRef() {

            referenceService.deleteReference(id)
                .then(function (data) {
                    console.log(data)
                }).catch(function (err) {
                    console.log(err);
                })
        }


        function _openExample(reference) {
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                component: 'currentReference',
                size: 'lg',
                resolve: {
                    item: function () {
                        return {
                            id: reference.id
                        };
                    }
                }
            });
            modalInstance.result.then(function () {
                //console.log("modal closed");
                vm.getAllRefByTypePag(vm.searchRefModel);
            }
                , function () {
                    //console.info();
                    vm.getAllRefByTypePag(vm.searchRefModel);
                }
            );
            function _toggleAnimation() {
                vm.animationsEnabled = !vm.animationsEnabled;
            }
        }
    }
})();


(function () {
    'use strict';

    // Please note that the close and dismiss bindings are from $uibModalInstance.
    angular.module('referenceApp').component('currentReference', {
        templateUrl: '/Scripts/templates/currentReferenceTemplate.html',
        bindings: {
            resolve: '<',
            close: '&',
            dismiss: '&'

        },
        controller: function (referenceService) {
            var vm = this;
            vm.$onInit = _init;
            vm.getCurrentRef = {};
            vm.edit = _edit;
            vm.submitEdit = _submit;
            vm.delete = _delete;

            function _delete() {
                vm.getCurrentRef.id = vm.resolve.item.id;
                referenceService.deleteReference(vm.getCurrentRef)
                    .then(function (data) {
                        console.log(data);
                        vm.disable = true;
                        vm.otherButtons = false;
                        vm.close({});
                    }).catch(function (err) {
                        console.log(err)
                    })
            }

            function _submit() {
                vm.getCurrentRef.id = vm.resolve.item.id;
                console.log(vm.getCurrentRef);
                referenceService.editReference(vm.getCurrentRef)
                    .then(function (data) {
                        console.log(data);
                        vm.disable = true;
                        vm.otherButtons = false;
                    }).catch(function (err) {
                        console.log(err)
                    })

            }
            function _edit() {
                vm.disable = false;
                vm.otherButtons = true;
            }
            function _init() {
                console.log(vm.resolve.item.id);
                referenceService.getCurrentRef(vm.resolve.item.id)
                    .then(function (data) {
                        console.log(data);
                        vm.getCurrentRef = data;
                        console.log(vm.getCurrentRef);
                        vm.disable = true;
                    }).catch(function (err) {
                        console.log(err)
                    })
            }



        }
    });
})();
