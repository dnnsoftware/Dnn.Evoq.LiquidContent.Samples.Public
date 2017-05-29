import angular from 'angular';
import './style.less';

const url = "https://qa.dnnapi.com/content/api/ContentItems";
const ctUrl = "https://qa.dnnapi.com/contentapi/ContentTypes";

let app = () => {
  return {
    template: require('./app.html'),
    controller: 'job-list-controller',
    controllerAs: 'app'
  }
};

class AppCtrl {
  constructor($scope, $http, $compile, $sce) {
    this.$http = $http;
    $scope.jobList = [];
    const template = require('./app.html');
    const templ = $compile(template)($scope);
    const $el = document.getElementById("dnn_ctr470_View_ScopeWrapper");
    $el.innerHTML = "";
    const token = $el.getAttribute("token");
    angular.element($el).append(templ);

    const APIkey = `Bearer ${token}`;
    $http.get(ctUrl, { headers: { authorization: APIkey } }).then((data) => {
      const contentTypeList = data.data.documents;
      const contentType = contentTypeList.find(ct => ct.name === "Job Posting");
      if (!contentType) {
        return console.log("No content Type 'Job Posting' found");
      }
      $http.get(`${url}?contentTypeId=${contentType.id}`, { headers: { authorization: APIkey } }).then((data) => {
        $scope.jobList = data.data.documents;
      });
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