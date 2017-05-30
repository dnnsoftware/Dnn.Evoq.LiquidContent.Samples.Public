import angular from 'angular';
import './style.less';

const pageSize = 2;

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
    this.$compile = $compile;
    this.$scope = $scope;
    $scope.jobList = [];
    $scope.currentIndex = 0;
    $scope.pages = [];
    $scope.showForm = false;
    $scope.buttonText = "Add New";
    $scope.formData = { jobTitle: "", description: "" };

    $scope.togglePanel = this.togglePanel.bind(this);
    $scope.onSubmit = this.onSubmit.bind(this);
    $scope.onRemove = this.onRemove.bind(this);
    $scope.onEdit = this.onEdit.bind(this);
    $scope.onCancelEdit = this.onCancelEdit.bind(this);
    $scope.onSave = this.onSave.bind(this);
    $scope.jobBeingEdited = null;
    $scope.hideModal = this.onHideModal.bind(this);
    $scope.confirmationModal = {
      show: false,
      text: "",
      onConfirm: () => {}
    }

    const template = require('./app.html');
    const templ = $compile(template)($scope);
    this.el = document.getElementsByClassName("job-list")[0];
    this.el.innerHTML = "";
    const token = this.el.getAttribute("token");
    angular.element(this.el).append(templ);

    this.APIkey = `Bearer ${token}`;

    this.pageSize = this.el.getAttribute("pagesize");
    $scope.mode = this.el.getAttribute("mode");
    this.getContentTypeId();

    $scope.toTrustedHTML = function (html) {
      return $sce.trustAsHtml(html);
    };

    this.createPages();
    this.addConfirmationModal();

    const selectPage = function (pageIndex) {
      $scope.currentIndex = pageIndex;
      this.loadPage();
    };

    $scope.selectPage = selectPage.bind(this);

    $scope.appliedClass = function (myObj) {
      if (myObj.pageIndex === $scope.currentIndex) {
        return "current-page";
      }
      return "";
    }
  }

  onSubmit() {
    const {jobTitle, description} = this.$scope.formData;
    const params = {
      contentTypeId: this.contentTypeId,
      description: "",
      name: jobTitle,
      tags: [],
      details: { jobTitle, description }
    };
    this.$http.post(`${url}?publish=true`, params, { headers: { authorization: this.APIkey } }).then((data) => {
      this.$scope.currentIndex = 0;
      this.loadPage();
      this.togglePanel();
    });
  }

  onEdit(job) {
    this.$scope.jobBeingEdited = JSON.parse(JSON.stringify(job));
  }

  onCancelEdit() {
    this.$scope.jobBeingEdited = null;
  }

  onSave (job) {
    this.$http.put(`${url}/${job.id}`, this.$scope.jobBeingEdited, { headers: { authorization: this.APIkey } }).then((data) => {
      job.details.description = this.$scope.jobBeingEdited.details.description;
      job.details.jobTitle = this.$scope.jobBeingEdited.details.jobTitle;
      this.$scope.jobBeingEdited = null;
    });
  }

  onRemove(job) {
    const {confirmationModal} = this.$scope;
    confirmationModal.text = "Are you sure you wont to remove the item?"
    confirmationModal.onConfirm = this.onDelete.bind(this, job);
    confirmationModal.show = true;
  }

  onHideModal() {
    this.$scope.confirmationModal.show = false;
  }

  onDelete(job) {
    this.$http.delete(`${url}/${job.id}`, { headers: { authorization: this.APIkey } }).then((data) => {
      this.$scope.currentIndex = 0;
      this.$scope.confirmationModal.show = false;
      this.loadPage();
    });
  }

  togglePanel() {
    this.$scope.showForm = !this.$scope.showForm;
    this.$scope.buttonText = this.$scope.showForm ? "Cancel" : "Add New";
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
    this.$http.get(`${url}?contentTypeId=${this.contentTypeId}&startIndex=${this.$scope.currentIndex * pageSize}&maxItems=${pageSize}`, { headers: { authorization: this.APIkey } }).then((data) => {
      this.$scope.jobList = data.data.documents;
      this.updatePages(data.data.totalResultCount);
    });
  }

  updatePages(totalResultCount) {
    const qtyPages = Math.ceil(totalResultCount / pageSize);
    const pages = [];
    for (let i = 0; i < qtyPages; i++) {
      pages.push({ pageNumber: i + 1, pageIndex: i });
    }
    this.$scope.pages = pages;
  }

  createPages() {
    const template = require('./pager.html');
    const templ = this.$compile(template)(this.$scope);
    angular.element(this.el).append(templ);
  }
  
  addConfirmationModal() {
    const template = require('./confirmationModal.html');
    const templ = this.$compile(template)(this.$scope);
    angular.element(this.el).append(templ);
  }
}

AppCtrl.$inject = ["$scope", "$http", "$compile", "$sce"];

const MODULE_NAME = 'app';

angular.module(MODULE_NAME, [])
  .directive('app', app)
  .controller('job-list-controller', AppCtrl);

export default MODULE_NAME;