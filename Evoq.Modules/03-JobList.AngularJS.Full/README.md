# Evoq Modules - Liquid Content Job List With Edit Functionality and Pegination

This is a sample DNN module to access Liquid Content using Angular JS v.1.6. A bearer token with read and write permission for Liquid Content is generated on the server side and passed to the front-end.
This module includes Edit, Add New and Remove functionality as well as pagination. Also it includes server side authontication to deside the mode for the application: "read" or "edit".
 
This documentation is a coninuation of documentation for the read-only Evoq.Module. Please refer to the previous [documentation](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples.Public/tree/master/Evoq.Modules/02-JobList.AngularJS.Simple)

![Screenshot1](images/screenshot1.png)
 

# Templates

## APP.html

Add button to the Header. This button will toggle the new Job form panel. Include the following derectives:
```
ng-hide="mode !== 'edit'" // hides the button if mode is not "edit"
ng-click="togglePanel($event)" // toggles the form panel on click
{{buttonText}} // Text will dinamicaly change depending on the state of the Panel
```

![Screenshot1](images/screenshot2.png)

Add the form panel. This panel will allow users to enter title and description of a new Job. This pannel will toggle on the button click.
Include the following derectives:

```
ng-hide="mode !== 'edit'" // hides the button if mode is not "edit"
ng-class="{'form-pane ': !showForm, 'form-pane show': showForm}" // adds "show" class to the element if showForm is true
ng-submit // calls provided funcction when the form is submitted
ng-model // bindes input field with the variablle provided
```

![Screenshot1](images/screenshot3.png)

Add Remove and Edit buttons to the job-item. Provide onRemove and onEdit function accordinaly 

![Screenshot1](images/screenshot4.png)

Add ng-hide directive to the job-item element. 
```
ng-hide="jobBeingEdited && job.id === jobBeingEdited.id"
```
It will hide default view and replace with the edit view when Edit button is clicked

![Screenshot1](images/screenshot6.png)

Add edit view for job-item. This view will replace the default job-item view when it's in edit mode
Add ng-hide directive to hide it when it's not in edit mode. To check if the item is in edit mode we compare jobBeingEdited.id with current job.id:
```
ng-hide="mode !== 'edit' ||!jobBeingEdited || job.id !== jobBeingEdited.id "
```
Add ng-model directive to bind the input elements to the variables provided:
```
ng-model="jobBeingEdited.details.jobTitle"
ng-model="jobBeingEdited.details.description"
```
Add save and cancel button that calls onSave() and onCancelEdit() functions accordingly:

![Screenshot1](images/screenshot5.png)

## PAGER.html

For pagination add pager.html template:

![Screenshot1](images/screenshot7.png)

```
ng-hide="pages.length === 1" // hides the pager if there is only one page 
ng-repeat="page in pages" // repeats for each page
ng-class="appliedClass(page)" // gets the class for each page. Class can be "current-page" if it's current page.
ng-click="selectPage(page.pageIndex)" // selects a page on click
```

## ConfirmationModal.html

Add ConfirmationModal.html will serve as a confirmation modal that pops up on Remove event. It will ask for confirmation before removing an item. 

Add "show" class to the modal if confirmationModal.show iqual to true.

![Screenshot1](images/screenshot8.png)

We assume that the modal will be used not only for confirmation of deletion operation. That's why we need dinamic text in the modal:
```
{{confirmationModal.text}}
```

![Screenshot1](images/screenshot9.png)

Add Yes and Cancel buttons. They will fire onConfirm() and hideModal() accordingly.

![Screenshot1](images/screenshot10.png)


# APP.js

## Pagenation

In the constructor of the controller add the following:

```
$scope.pages = []; // sotres apge list 
$scope.currentIndex = 0; //stores information about current page
```
![Screenshot1](images/screenshot11.png)

Get page size from the Evoq.Module settings

![Screenshot1](images/screenshot12.png)

Add selectPage function that will fire when a page is clicked and it will change currentIndex and load job list items for the page.

![Screenshot1](images/screenshot13.png)

Add appliesClass function that changes class of a button to "current-page" if the page is current.

![Screenshot1](images/screenshot14.png)

Add createPages method to the controller class. This method append pager template to the DOM and compiles it using $compile service

![Screenshot1](images/screenshot15.png)

Call the createPages method from the construcor of the controller class:

![Screenshot1](images/screenshot16.png)

## Confirmation modal and Remove Functionality

Confirmation modal will be used for confirmation of the delete operation.
Add confirmationModal object to the $scope in the constructor of the controller.

![Screenshot1](images/screenshot17.png)

Add addConfirmationModal method to the controller class. This method appends the Modal template to the DOM and compiles it using $compile service

![Screenshot1](images/screenshot18.png)

Add onRemove function(). This function is called when Remove button is clicked. This function defines text for the modal, onCofirm function. And it shows the module.

![Screenshot1](images/screenshot19.png)

Add onDelete method to the controller class. This method will be called after user clicks Yes button in the Confirmation Modal.
This method calles delete API using job.id and APIkey. After the item is deleted it calles loadPage() function Asynchronosly.

![Screenshot1](images/screenshot20.png)

## Add New Job Item

In the constructor of the controller add showForm and ButtonText variables:

```
$scope.showForm = false;
$scope.buttonText = "Add New";
```

![Screenshot1](images/screenshot21.png)

Add formData object. This object will keep new job item information.
```
$scope.formData = { jobTitle: "", description: "" };
```
![Screenshot1](images/screenshot23.png)

Add togglePanel method to the controller class. This method will be called when Add New button is clicked.
It toggles the new item form panel and changes text of the button to "Cancel" or "Add New" depending on wether the panel is opened or closed.

![Screenshot1](images/screenshot22.png)

Add onSubmites method to the controller class. This method is called when user clickes Add button under the new item form.
It submitse the data user is added and calles post request to the server.
After post request returnes result, it resets pagination, setting currentIndex to 0 and toggles the form pannel to close it.

![Screenshot1](images/screenshot24.png)

## Edit Job Item

In the constructor of the controller add jobBeingEdited variable. This object will keep information about what job item is edited in curent moment.

![Screenshot1](images/screenshot25.png)

Add onEdit(), onCancelEdit() and onSave() methods to the controller class.

*onEdit()* method is called when user clicks the Edit button. It clones the job item object to the jobBeingEdited object. 

*onCancelEdit()* method is called when user clickes Cancel button. It resets jobBeingEdited to null

*onSave()* method is called when user clicks Save button. It calles put request to the server, using jobBeingEdited object as a parametr. After the call is done it resetes all paramtres of job item to parametres of jobBeingEdited. After that it resets jobBeingEdited to null.

![Screenshot1](images/screenshot27.png)








