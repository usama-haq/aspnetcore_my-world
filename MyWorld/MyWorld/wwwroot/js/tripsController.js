﻿// tripsController.js
(function () {
    "use strict";

    // Getting the existing module
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;

        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function (response) {
                // Success
                angular.copy(response.data, vm.trips);
            }, function (error) {
                // Failure
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Add a new Trip
        vm.addTrip = function () {
            vm.errorMessage = "";
            vm.isBusy = true;

            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                }, function (error) {
                    vm.errorMessage = "Failed to save new trip: " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }
})();