import angular from 'angular';
import './style.less';

const url = "https://qa.dnnapi.com/content/api/ContentItems";

let app = () => {
  return {
    template: require('./app.html'),
    controller: 'job-list-controller',
    controllerAs: 'app'
  }
};

class AppCtrl {
  constructor($scope, $http, $compile,$sce) {
    $scope.jobList = [];
    const template = require('./app.html');
    const templ = $compile(template)($scope);
    const $el = document.getElementsByClassName("job-list")[0];
    $el.innerHTML = "";
    const token = $el.getAttribute("token");
    angular.element($el).append(templ);

    const APIkey = `Bearer ${token}`;
    $http.get(`${url}?contentTypeId=a8980264-e974-41fa-8de3-6ced4e935ef2`, { headers: { authorization: APIkey } }).then((data) => {
      $scope.jobList = data.data.documents;
      console.log($scope.jobList);
    });
    $scope.toTrustedHTML = function (html) {
      return $sce.trustAsHtml(html);
    };
  }
}

AppCtrl.$inject = ["$scope", "$http", "$compile", "$sce"];

const MODULE_NAME = 'app';

angular.module(MODULE_NAME, [])
  .directive('app', app)
  .controller('job-list-controller', AppCtrl);

export default MODULE_NAME;