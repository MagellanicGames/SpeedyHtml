# Speedy HTML Builder


The point of this little program is to make a quick respectible site with minimal effort.  Websites made with this tool
will scale across all device types.  Small mobile screens up to large HD desktop screens.  This is of course all thanks
to Bootstrap.  Sites generated with this tool use the CDN for Bootstrap.

This tool should allow you to focus simply on the content, not "is this paragraph centered?". Obviously functionality is limited
but its for basic needs, temporary sites or simply as base template to build a better webpage.

The program takes input of an .shb file.
Simply run .shb script files from a console like so: SpeedyHtmlBuilder.exe script.shb

The structure of the website follows basic Bootstrap grid structure.

Containers must be added to contain any content, except for raw html.

Containers hold rows and any number of rows can be added to a container.

Any sections of raw html must be between an htmlStart and htmlEnd.
This is for easier customisation, such as adding navigation bars etc.

## Building

Just pull the project.  Open in Visual Studio 2015, select Release and build.  Move the built exe to where is easiest to use.

# Step-by-Step Example

This section will make a very basic example site.

### Step1


First line contains title and stylesheet.

```
My Awesome Website Title,Style.css
```
To use the default stylesheet simply state the website title.

```
My Awesome Website Title
```
### step2 - Create a container

```
addContainer;
```
### Step3 - Add a heading
```
heading(text:My Awesome Website Heading,class:myCssClass);
```

### Step4 - Add a sub heading

```
subHeading(text:My Article Title,class:myCssClass);
```

### Step5- Add an image

```
addImage(myimg.gif);
```

### Step6 - Add a row with a paragraph and padding above

```
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
```

### Step7 - Add some more padding then a footer

```
padding;

addFooter(email:myEmail@mail.com,date:01/07/1999,copy:Web Developer);
```

### Step8 - Save with the .shb extension and run through Simple Html builder

```
SpeedyHtmlBuilder.exe websiteScript.shb
```


# Syntax

## First Line

First line contains the title and an optional CSS stylesheet name:

```
WebsiteTitle, Style.css
```

## Add a Container

```
addContainer;
```
## Add a Heading

```
addHeading(text:My Title,class:red);
```
To pass no class, simply leave blank:

```
addHeading(text:My Title,class:);
```

## Add an Image

To add an image on its own row in a single line simply requires the following syntax:

```
addImage(imageName/url);
```
Does not require rowStart or rowEnd.

## Add Padding

If using the default style sheet, simply use the following command to add padding:

```
padding();
```
Does not require rowStart or rowEnd.

## Footer

Add a footer containing email, update date and copyright notice:

```
addFooter(email:email@mail.com,date:01/07/1999,copy:yourName);
```

Does not require a call to rowStart. Does need to be within a container.

## Start a Row

A container must be added before calling.

```
rowStart;
```

Content html goes after add.

## Close a Row

Before starting a new row, the old row must be closed off with:

```
rowEnd;
```

## Adding Raw HTML

This command is used for adding bare html to a site and will not be added to any containers.
Useful for adding html that doesn't need to be within the main page structure.
Follows same structure as rows:

```
htmlStart;
```

Html goes in between. then close off with:

```
htmlEnd;
```

### Css Classes


## Containers

To add a css class to a container:
```
addContainer;{class:myCssClass1}
```
Just as with typical html/css, to add multiple class, simply seperate them with a space:
```
addContainer;{class:myCssClass1 myCassClass2}
```

## Rows

Css classes work the same as for containers, just attach them to the rowStart:
```
rowStart;{class:myCssClass}
```
Multiple classes are the same as with containers, simply list, seperating with spaces:
```
rowStart;{class:myCssClass1 myCssClass2}
```
