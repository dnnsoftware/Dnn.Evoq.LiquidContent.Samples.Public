import angular from 'angular';
import './style.less';

const url = "https://qa.dnnapi.com/content/api/ContentItems";
const ctUrl = "https://qa.dnnapi.com/contentapi/ContentTypes";

const moduleId = window.dnn.moduleIds[window.dnn.moduleIds.length - 1]

if (typeof window.app === "undefined") {
  window.app = {}
}

window.app[moduleId] = () => {
  return {
    template: require('./app.html'),
    controller: 'job-list-' + moduleId,
    controllerAs: 'app'
  }
};

window.app["ctl_" + moduleId] = class {
  constructor($scope, $http, $compile, $sce) {
    this.$http = $http;
    this.$compile = $compile;
    this.$scope = $scope;
    $scope.jobList = [];

    const template = require('./app.html');
    const templ = $compile(template)($scope);
    this.mainEl = document.getElementById(moduleId);
    this.el = document.getElementById("ang-" + moduleId);
    this.el.innerHTML = "";
    angular.element(this.el).append(templ);
    const token = this.mainEl.getAttribute("token");
    this.APIkey = `Bearer ${token}`;
    this.getContentTypeId();
    $scope.toTrustedHTML = function (html) {
      return $sce.trustAsHtml(html);
    };
  }

  getContentTypeId() {
    this.$http.get(ctUrl, { headers: { authorization: this.APIkey } }).then((data) => {
      const contentTypeList = data.data.documents;
      const contentType = contentTypeList.find(ct => ct.name === "Job Posting");
      if (!contentType) {
        return console.log("No content Type 'Job Posting' found");
      }
      this.contentTypeId = contentType.id;
      this.loadPage();
    });
  }

  loadPage() {
    this.$http.get(`${url}?contentTypeId=${this.contentTypeId}`, { headers: { authorization: this.APIkey } }).then((data) => {
      this.$scope.jobList = data.data.documents;
    });
  }
}

window.app["ctl_" + moduleId].$inject = ["$scope", "$http", "$compile", "$sce"];

const MODULE_NAME = moduleId;

angular.module(MODULE_NAME, [])
  .directive('app-' + moduleId, app)
  .controller('job-list-' + moduleId, window.app["ctl_" + moduleId]);

clearTimeout(window.timeOut);
window.timeOut = setTimeout(() => {
  angular.bootstrap(document, window.dnn.moduleIds);
}, 100);

export default window.app[moduleId];