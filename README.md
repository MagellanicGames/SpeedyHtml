# Speedy HTML Builder


The point of this little program is to make a quick respectible site with minimal effort.  Websites made with this tool
will scale across all device types.  Small mobile screens up to large HD desktop screens.  This is of course all thanks
to Bootstrap.  Sites generated with this tool use the CDN for Bootstrap.

The program takes input of an .shb file.
Simply run .shb script files from a console like so: SimpleHtmlBuilder.exe script.shb

The structure of the website follows basic Bootstrap grid structure.

Containers must be added to contain any content, except for raw html.

Containers hold rows and any number of rows can be added to a container.

Any sections of raw html must be between an htmlStart and htmlEnd.
This is for easier customisation, such as adding navigation bars etc.


## Syntax

This section will make a very basic example site.



### Step1


First line contains title and stylesheet

e.g: MyAwesomeWebsiteTitle,myUniqueStyleSheet.css



### step2 - Create a container


addContainer;


### Step3 - Add a row with a title

rowStart;

<center><h1 id="blink">My Awsome Website</h1></center>

rowEnd;


### Step4 - Add a row with a paragraph and padding above


padding;
rowStart;

<p> Here is my website, it is really fantastic and complicated.  Only a genius could make a website this
sophisticated and pretty.Here is my website, it is really fantastic and complicated.  Only a genius could make a website this
sophisticated and pretty.Here is my website, it is really fantastic and complicated.  Only a genius could make a website this
sophisticated and pretty.Here is my website, it is really fantastic and complicated.  Only a genius could make a website this
sophisticated and pretty.
Here is my website, it is really fantastic and complicated.  Only a genius could make a website this
sophisticated and pretty.
</p>
rowEnd;


### Step5 - Add some more padding then a footer


padding;
addFooter;{email:myEmail@mail.com,date:01/07/1999,copy:Web Develop}



### Step6 - Save with the .shb extension and run through Simple Html builder



SimpleHtmlBuilder.exe websiteScript.shb


## Css Classes


### Containers

To add a css class to a container:

addContainer;{class:myCssClass1}

Just as with typical html/css, do add multiple class, simply seperate them with a space:

addContainer;{class:myCssClass1 myCassClass2}


### Rows

Css classes work the same as for containers, just attach them to the rowStart:

rowStart;{class:myCssClass}

Multiple classes are the same as with containers, simply list, seperating with spaces:

rowStart;{class:myCssClass1 myCssClass2}
