// Ionic Starter App
const url = "https://qa.dnnapi.com/content/api/ContentItems";
const contentTypeId = "36751f3b-7d3c-4cb5-bbab-40809013c989";
const maxItems = 1;
const APIkey = "Bearer 1e4f8360d5761bee3df67eab656b32a3";

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
var app = angular.module('moviesApp', ['ionic']);
app.controller("moviesController", function ($scope, $http, $timeout, $ionicScrollDelegate) {
  $scope.movies = [];
  $scope.startIndex = 0;
  $scope.lastId = "";
  $scope.showForm = "";
  $scope.noMoreItemsAvailable = false;
  $scope.formData = {movieName: "", description: "", score: "", image: ""};

  $scope.appliedClass = function (myObj) {
    if (myObj.id === $scope.lastId) {
      return "new-item";
    }
    return "";
  }

  $scope.onSubmit = function() {
    const {movieName, image, description, score} = $scope.formData;
    const params = {
      contentTypeId,
      description: "",
      name: movieName,
      tags: [],
      details: {movieName, image, description, score}
    };
    $http.post(`${url}?publish=true`, params, {headers: { authorization: APIkey }}).then(function(data) {
      console.log(data)
      $scope.movies = [data.data, ...$scope.movies ];
      $scope.togglePanel();
      $ionicScrollDelegate.scrollTop(true);
    });
  }


  $scope.loadMore = function () {
    $http.get(`${url}?startIndex=${$scope.startIndex}&maxItems=${maxItems}&fieldOrder=createdAt&orderAsc=false`, { headers: { authorization: APIkey } }).then(function (data) {
      $scope.movies = [...$scope.movies, ...data.data.documents];
      $scope.startIndex = $scope.startIndex + maxItems;
      $scope.noMoreItemsAvailable = $scope.movies.length === data.data.totalResultCount;
      $scope.lastId = data.data.documents[0].id;
      $timeout(function () {
        $scope.lastId = "";
      }, 100);
      $scope.$broadcast('scroll.infiniteScrollComplete');
    });
  }

  $scope.togglePanel = function () {
    $scope.showForm = $scope.showForm ? "" : "show-form";
  }
});

app.run(function ($ionicPlatform) {
  $ionicPlatform.ready(function () {
    if (window.cordova && window.cordova.plugins.Keyboard) {
      // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
      // for form inputs)
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

      // Don't remove this line unless you know what you are doing. It stops the viewport
      // from snapping when text inputs are focused. Ionic handles this internally for
      // a much nicer keyboard experience.
      cordova.plugins.Keyboard.disableScroll(true);
    }
    if (window.StatusBar) {
      StatusBar.styleDefault();
    }
  });
})
