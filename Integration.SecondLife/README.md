# Project: DNN Liquid Content Subscribo in Second Life


## Background

Several years ago, [Second Life](http://www.secondlife.com)® residents were limited to 25 groups that they can join. Joining groups were necessary for many things, including getting freebies from a vendor, getting access to a parcel or a region, getting permissions to terraform land or modify objects, getting notices about events or new products or store discounts, and many others. Eventually, Second Life increased the allowed number of groups to 42, which is still not enough for some residents.

![freebie merchandise that require group membership - Maitreya](images/maitreya-freebie.png)
**_Figure 1_ - Freebie merchandise like these are usually given away to members of a store group to get customers to come back or to introduce newbies to the brand. Location: http://maps.secondlife.com/secondlife/Maitreya%20Isle/122/190/23 **

To capture users who don't want to use up their group slots, a few scripters have created *subscribos*, which are objects that residents touch to be added to a mailing list, which is stored in some database off-world (i.e., Google Sheets).

Similar techniques have been used for scripted vendors, which are inworld objects that sell merchandise. Vendors can be created without scripts; however, scripts can track what customers have bought inworld, so that they could be automatically sent updates to the product.

This also allows merchants to review their sales without having to do much bookkeeping. Inworld transactions in Linden dollars (the inworld currency) are kept on the Second Life database for only about a month. Then they are deleted forever. So, scripted vendors eliminate the worry that the record of a sales transaction is lost, just because the merchant was stranded for a few months on a remote tropical island with no internet access.

![a subscribo in the Second Life Marketplace](images/marketplace-subscribo.png)
**_Figure 2_ - One of several subscribo brands in the SL Marketplace.**


## DNN Subscribo

The DNN Subscribo is a basic subscribo, that uses Liquid Content™ to store customer information as content items. This product can be expanded into an inworld vendor that is also a sales and customer tracking system.


## Components

On the DNN side:

- A content type to track subscribers:
  - The SL username can be used as the content item name. This will also be used to search for a specific subscriber (content item), if the content item ID is not known.
  - A single-line field to hold the SL UUID of the subscriber. This UUID is required by many Second Life APIs for private communications, product delivery, etc.
  - A multiple-choice field for mailing lists. Allow users to choose more than one, or set to **Checkboxes**.
  ![Screenshot of a content type definition in Evoq](images/contenttypescreenshot.png)
  **_Figure 3_ - Content Type for the subscriber information.**

On the Second Life side:

- A textured cube, that is configured so that:
  - Face #0 serves as the SUBSCRIBE button,
  - Face #1 serves as the UNSUBSCRIBE button, and
  - Face #3 displays the company logo.
  ![A textured and tortured cube in Second Life with settings](images/prim-settings.png)
  **_Figure 4_ - Settings for our cube as described.**

- A [Linden Scripting Language](http://wiki.secondlife.com/wiki/LSL_Portal) (LSL) script. LSL is the programming language used in Second Life and other OpenSim worlds. (OpenSim is a technology based on SL.) The LSL script has dependencies on the way the object is configured. You can create the same interactive object many ways:
  - You can use multiple prims that are combined into one object, and **a different action is performed depending on which prim is touched**. Con: Parcels and regions have limitations on land impact. A basic prim equals one land impact (LI). The LI of *mesh* objects vary, depending on the number of tris on the mesh. So users prefer objects that use as few prims or LI as possible. (Objects attached to avatar do not have LI limitations, although complex attachments can cause lag.)
  - You can also use just one face of the cube, and **a different action is performed depending on what part of the face is touched**. Con: This requires a more complex script that calculates the point that was touched and then matching it with some data structure that defines which points should do which action.
  - Our script ([DNNSubscriboForSL.txt]) assigns **different faces of a single prim (a cube) to do different actions**, which uses only one LI and less complex code.


## APIs Used

The core functionality of the LSL script uses the following APIs:

- In Liquid Content:
  - [GET /api/ContentItem?name=…](http://www.dnnsoftware.com/docs/api/api-contentitems-get.html)
  - [GET /api/ContentItem/{id}](http://www.dnnsoftware.com/docs/api/api-contentitems-id-get.html)
  - [PUT /api/ContentItem/{id}](http://www.dnnsoftware.com/docs/api-contentitems-id-put.html)
  - [POST /api/ContentItem](http://www.dnnsoftware.com/docs/api/api-contentitems-post.html)
- In LSL:
  - [llHTTPRequest() function](http://wiki.secondlife.com/wiki/LlHTTPRequest)
  - [http\_response event](http://wiki.secondlife.com/wiki/Http_response)
  - Various [JSON-related APIs](http://wiki.secondlife.com/wiki/Json_usage_in_LSL) to parse responses and modify them.


## LSL Script

For the complete script, see [DNNSubscriboForSL.txt].

The following are the DNN-related variables used by the script:
``` Linden Scripting Language
string URLDNNSOFTWARE = "http://www.dnnsoftware.com/";
string TGTDOMAIN = "https://dnnapi.com/content/api";
string DNNCONTENTTYPEID = "00000000-0000-0000-0000-000000000000";
string DNNCONTENTTYPENAME = "SL User";

list BASIC_HTTP_PARAMETERS = [
    HTTP_MIMETYPE, "application/json",
    HTTP_BODY_MAXLENGTH, 16384,   // max length for Mono
    HTTP_PRAGMA_NO_CACHE, TRUE,
    HTTP_CUSTOM_HEADER, "Authorization", "Bearer 00000000000000000000000000000000"
    ];
```
**NOTE:** You must replace the `DNNCONTENTTYPEID` with the UUID of the content type you create in your DNN site. You must also replace the "Bearer" UUID with [your own API key](https://www.dnnsoftware.com/docs/administrators/structured-content/create-api-key.html).


The following are the HTTP Requests:

**GET using a query**
``` Linden Scripting Language
t_kHttpReqGet = llHTTPRequest(
    TGTDOMAIN + "/ContentItems?name=" + (string) t_sUsername,
    BASIC_HTTP_PARAMETERS + [ HTTP_METHOD, "GET" ],
    "" );
```

**GET using the content item UUID of the specific "SL User"**
``` Linden Scripting Language
t_kHttpReqGet = llHTTPRequest(
    TGTDOMAIN + "/ContentItems/" + t_kDNNID,
    BASIC_HTTP_PARAMETERS + [ HTTP_METHOD, "GET" ],
    "" );
```

**POST**
``` Linden Scripting Language
t_kHttpReqPost = llHTTPRequest(
    TGTDOMAIN + "/ContentItems?publish=true",
    BASIC_HTTP_PARAMETERS + [ HTTP_METHOD, "POST" ],
    sPostStr );
```

**PUT**
``` Linden Scripting Language
t_kHttpReqPut = llHTTPRequest(
    TGTDOMAIN + "/ContentItems/" + t_kDNNID + "?publish=true",
    BASIC_HTTP_PARAMETERS + [ HTTP_METHOD, "PUT" ],
    sPostStr );
```

To parse the JSON structure returned in the HTTP Response, insert the parsing code (using `llJsonGetValue()`) in the `http_response` event handler.

![Flowchart](images/DNNSubscriboFlowchart.png)
**_Figure 5_ - Flowchart of the LSL script.**


## Prerequisites

- An Evoq site.
- Superuser/Host access to your site.
- A Second Life account.
- Some knowledge of how to create and manipulate prims.
- Some knowledge of how to script in the Linden Scripting Language. (Or copy the script from [DNNSubscriboForSL.txt].)
- 10L$ (10 Linden dollars = approximately US$0.04) to upload a texture.


## Steps

1. In Evoq, create a content type and an API key.

    1. [Create a content type](http://www.dnnsoftware.com/docs/administrators/structured-content/create-content-type.html). _See Figure 3._
        1. Set the content type name to *SL User*.
        2. Create the following fields:
            1. (Single-line)  Name: *SL UUID*
            2. (Multiple Choice) Name: *Subcriptions*
                Appearance: Checkboxes
                Choices: *inactive*, *customer*, *creator*, *announcements*, *gifts*, etc.
                (The script uses *announcements*.)

    2. [Create an API key](https://www.dnnsoftware.com/docs/administrators/structured-content/create-api-key.html).

2. In a graphics program, create a square texture.

    1. The bottom 1/8 as a red stripe for unsubscribing.

    2. The next higher 1/8 as a green stripe for subscribing.

    3. The remaining space to indicate what mailing list people would be subscribing to.
        NOTE: Save the texture as .jpg (not .png). The image must not be more than 2048x2048 pixels, which will be automatically shrunk to 1024x1024 during upload.

    Sample texture:
    (You can request a full-perms copy of this texture inside Second Life from *DNNDiaNNa*.)
    ![DNN Subscribo texture](images/Evoq-SubscribeUnsubscribe.jpg)
    **_Figure 6_ - Sample texture to be applied on the prim for the subscribo.**

3. Inside Second Life, put the subscribo together.

    1. Upload the texture.

    2. Create the object.
        1. Rez a cube.
        2. Torture the cube.
            * Size: &lt;0.5, 0.60396, 0.01&gt;
            * Taper: x = 0.0, y = 0.8
            * Top Shear: x = 0.0, y = -0.2
        3. Apply the texture. Adjust the texture scale and offset for each of the active faces to show only the relevant part of your texture.

    3. Create a new script.
        1. Create a new script in the Contents tab of the object or in your inventory.
        2. Copy and paste the LSL code to the script you created (replacing the default code) and save.
        3. In the script, replace:
            * The Content Type ID (DNNCONTENTTYPEID variable in Line 17/16).
            * The API key (HTTP\_CUSTOM\_HEADER number at the end of Line 24/23).
            (Note that the LSL editor inworld starts with Line 0 as the first line.)
            Both of these numbers are linked to your Evoq site.
        4. You can make additional changes as you wish.

    4. Set the permissions of the script and the object itself.


## Related links

- DNN Documentation Center: [Creating Apps That Use Microservices](http://www.dnnsoftware.com/docs/developers/microservices/creating-apps-that-use-microservices.html)
- [SecondLife.com](http://secondlife.com)
- Second Life Wiki: [LSL Portal](http://wiki.secondlife.com/wiki/LSL_Portal)

![DNN DiaNNa with completed subscribo](images/dnndianne-with-final-subscribo.png)
**_Figure 7_ - DiaNNa (username:dnndianna) inside Second Life with the completed subscribo.**

[DNNSubscriboForSL.txt]: DNNSubscriboForSL.txt