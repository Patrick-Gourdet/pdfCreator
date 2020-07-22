# THIS IS A SERVICE WHICH LETS YOU CONVERT A JSON STRING TO PDF
[![Build Status](https://img.shields.io/badge/Development-build-green)](https://fedigital.org)</br>
#Use Case Given a JSON String with Format, Header Body, Text Element defined as seen below. Will Create a PDF accordingly. All available attributes are also depicted below in the documentation.  
```
{
  "PageSize": "A4",
  "PageMargins": {
    "Top": 20,
    "Bottom": 20,
    "Left": 20,
    "Right": 20
  },
  "Elements": [
    {
      "$type": "EsImage",
      "Base64": "",
      "Align": "Center",
      "Width": 75
    },
    {
      "$type": "ESTEXT",
      "Content": "Unparalleled Property Services",
      "FontName": "Verdana",
      "FontSize": 8,
      "Align": "Center"
    },
    {
      "$type": "esline",
      "WidthPercent": 50,
      "Align": "Center",
      "Margins": {
        "Top": 20,
        "Bottom": 30
      }
    },
    {
      "$type": "ESTEXT",
      "Content": "Questionnaire",
      "FontName": "Verdana",
      "FontSize": 14,
      "Align": 2
    },
    {
      "$type": "esline",
      "WidthPercent": 50,
      "Align": "Center",
      "Margins": {
        "Top": 20,
        "Bottom": 30
      }
    },
    {
      "$type": "ESTEXT",
      "Content": "The Township Community Mastre Association, Inc.",
      "FontName": "Verdana",
      "FontSize": 20,
      "Align": 2
    },
    {
      "$type": "ESTEXT",
      "Content": "\"Proudly managed by Castle Management LLC.\"",
      "FontName": "Verdana",
      "FontSize": 20,
      "Align": 2
    },
    {
      "$type": "EsTable",
      "ColumnsWidthPercs": [
        50,
        20,
        30
      ],
      "HeaderCells": [
        {
          "Elements": [
            {
              "$type": "EsText",
              "Content": "The certificate has been prepared"
            }
          ]
        },
        {
          "Elements": [
            {
              "$type": "EsText",
              "Content": "Date 00\\00\\00"
            }
          ]
        }
      ],
      "BodyCells": [
        {
          "Elements": [
            {
              "$type": "EsText",
              "Content": "Pwner(s):"
            }
          ]
        },
        {
          "Elements": [
            {
              "$type": "EsText",
              "Content": "Yo Mama"
            }
          ]
        },
        {
          "Elements": [
            {
              "$type": "EsText",
              "Content": "Buyer(s)"
            }
          ]
        },
        {
          "Elements": [
            {
              "$type": "EsText",
              "Content": "Yo Daddy"
            }
          ]
        },
        {
          "Elements": [
            {
              "$type": "EsText",
              "Content": "Address"
            }
          ]
        },
        {
          "Elements": [
            {
              "$type": "EsText",
              "Content": "Up S*&^t Creeks Ave 666 DoomsVille"
}]}]}]} 
```

## Should you have any questions or need help setting up dont hesitate to write 
## @admin@irondigital.dev

---
title: Elder Scroll (ES)
---

Patrick Gourdet 12/12/2018

**Objective**
=============

Construction and configuration of a DLL whose purpose is to create a PDF
document out of a serialized JSON file.

**Functionality**
=================

Elder Scrolls defines a JSON format to specify the document structure and
contents. Following a detailed list of features by release.

Version 1.0
-----------

-   EsGenerator renders documents based on Json or Object structures.

-   Allows for the following configurations.

-   Page:

    -   Margins, Orientation

-   Text:

    -   Sizing, Color, Spacing (letter and words), Font style, Bold, Italic,
        Underline , Strike Through, Full Borders, Alignment.

-   Borders:

    -   Border width, Dotted, Dashed, Solid, Sides(TOP, Bottom, Left, Right,
        All, None), Color, opacity

-   Line Separators:

    -   Thickness, Color, Length to span

-   Images:

    -   Need to be in Base64, Width, Height, Alignment

    -   Lists

    -   A bullet point list that contains text for multiple levels the list
        itself must contain alist.

-   An object model matching the document elements definitions.

-   An extensible model to create more elements.

**Future Releases**
-------------------

-   Ability to append an external PDF into the generated one.

-   Create and use pre-defined styles.

-   Define and use replaceable variables into the document.

-   Add support for TotalNumberOfPages in Header/Footer.

**Usage**
=========

The EsGenerator is a static class that has the rendering capabilities for the
final document.

Render a JSON document to a binary
----------------------------------

Method Format: byte[] Render(string jsonDocument)

Render a JSON document to a Stream
----------------------------------

Method: void Render(string jsonDocument, Stream stream)

Render a JSON document to a File
--------------------------------

Method: void Render(string jsonDocument, string filePath)

Render a Document to a Stream
-----------------------------

Method: void Render(EsDocument document, Stream stream)

Render a Document to a File
---------------------------

Method: void Render(EsDocument document, string filePath)

**Model**
=========

EsDocument
----------

EsDocument is the container upon which all elements of the PDF are to be set.

**Properties**

The order of the attribute within a element does not mater nor does the casing
of the given attribute. Example: “COLOR”, “color”, “ColOR”

All the properties are optional otherwise indicated.

| Name        | Type               | Description                                                                                                                                                                                              |
|-------------|--------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| EmbedFonts  | Boolean            | True to embed the fonts into the document otherwise false (default).                                                                                                                                     |
| PageSize    | String             | The name or the size of the page to use. This argument excepts all Norms of pages indicated on Names: The default is A4, Letter, EXECUTIVE, LEDGER, LEGAL, TABLOID, A0 through to A10, B0 through to B10 |
| Rotate      | Boolean            | True to rotate the page otherwise false (default) The default is Portate.                                                                                                                                |
| Header      | EsTableCell        | When the content attribute is set to {{PageNumber}} this will be replaced with actual current page number.                                                                                               |
| Footer      | EsTableCell        | When the content attribute is set to {{PageNumber}} this will be replaced with actual current page number.                                                                                               |
| PageMargins | EsMargin           | Creating the margin for the current document.                                                                                                                                                            |
| Sections    | List of EsElements | A section is a list of ESSection within which an ELement may be placed                                                                                                                                   |

**EsSection**
-------------

EsSection is an object container which allows the addition of all other
EsElements . First one must add the EsSection to the document which then allows
to add the elements in return will construct the rest of the document. The only
things that are not to be included into the EsSection block is the header,
footer and the page initiation attributes.

**Properties**

All the properties are optional otherwise indicated.

| Name     | Type             | Description                        |
|----------|------------------|------------------------------------|
| Elements | List of Elements | A list of Elements to be rendered. |

**Elements**
============

The element is the most basic. Each element must declare it type in the JSON
format using the “\$type” property. The element values are all to be entered
without the \<\> Example: \<Document Text\> = I want to display this.

**EsText**
----------

“\$type”: “EsText”

**Properties**

EsText is the smallest element within ElderScroll which accepts almost all other
element attributes.

| Name    | Type   | Description               |
|---------|--------|---------------------------|
| Content | String | The content of an element |

**Inherited Properties**

| EsSyleElement Properties |         |                                                                                                               |
|--------------------------|---------|---------------------------------------------------------------------------------------------------------------|
| Name                     | Type    | Description                                                                                                   |
| BackGroundColor          | String  | The attribute receives the wished color in string format.                                                     |
| Bold                     | Bool    | When set makes the font bold                                                                                  |
| Borders                  | String  | Is a by comma separated string value. “Left, Right” will result in a border left and right of the text object |
| CharSpacing              | Float   | This allows to modify the finite space between two consecutive letters.                                       |
| Color                    | String  | The attribute receives the wished color in string format.                                                     |
| FontName                 | String  | Font Styling from wished font family                                                                          |
| FontSize                 | Integer | An integer value which represents the Font Size                                                               |
| HorizontalAlign          | String  | Like TextAlign this attribute is set to align the element on a Document                                       |
| Italic                   | Bool    | This is set to true or false in invoke the italic property                                                    |
| Opacity                  | Float   | This is a value between 0 and 1 and dictates the visual intensity of an element.                              |
| StrokeColor              | String  | Changes the current color for stroking paths                                                                  |
| TextAlign                | String  | This can either be set to left, right or center using the afore mentioned in string format.                   |
| UnderLine                | Bool    | Set to true the text then is underlined otherwise not.                                                        |
| BorderStyle              | String  | This string must be set to either Solid, Dotted or Dashed the default is Solid.                               |
| WordSpacing              | Float   | This value sets the spacing between the words in a content attribute.                                         |

**EsParagraph** 
----------------

“\$type”: “EsParagraph”

**Properties**

EsParagraph is a list of EsTexts.

| Name  | Type           | Description                              |
|-------|----------------|------------------------------------------|
| Texts | List of EsText | A list of Texts that form the Paragraph. |

**Inherited Properties**

| ESBlockElement   |                 |                                                                                                                                                                                                                                                                                                                                                                                              |
|------------------|-----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Name             | Type            | Description                                                                                                                                                                                                                                                                                                                                                                                  |
| HeightPerc       | Float           | When this value is set it can magnify or reduce the height of and element by a percentage value.                                                                                                                                                                                                                                                                                             |
| HeightPoint      | Float           | When this value is set it can magnify or reduce the height of and element by a percentage value.                                                                                                                                                                                                                                                                                             |
| KeepTogether     | Bool            | Will force to keep elements as close as possible to one another.                                                                                                                                                                                                                                                                                                                             |
| KeepTogetherNext | Bool            | The start of the next sibling of this element should be placed in the same area.                                                                                                                                                                                                                                                                                                             |
| Margins          | EsMargins       | Sets the margins around the element to a series of new widths.                                                                                                                                                                                                                                                                                                                               |
| MaxHeight        | Float           | This constrains the height variable to not exceed provided value.                                                                                                                                                                                                                                                                                                                            |
| MinHeight        | Float           | This constrains the height variable to not drop below provided value.                                                                                                                                                                                                                                                                                                                        |
| Padding          | EsPadding       | This allows to set padding for all four sides of an element. Further more one must only set All, or if alternating sides are required set the all property and then the value of the side which is supposed to be different.                                                                                                                                                                 |
| SpacingRatio     | Float           | Sets a ratio which determines in which proportion will word spacing and character spacing be applied when horizontal alignment is justified. This is a value between 0 and 1 and determines how the free space is to be dispersed between char spacing and word spacing. 1 means no additional spacing char spacing will be applied and o means no additional word spacing will be assigned. |
| VerticalAlign    | EsVerticalAlign | Sets the vertical alignment of the element                                                                                                                                                                                                                                                                                                                                                   |
| WidthPerc        | Float           | When this value is set it can magnify or reduce the width of and element by a percentage value.                                                                                                                                                                                                                                                                                              |
| WidthPoint       | Float           | When this value is set it can magnify or reduce the width of and element by a percentage value.                                                                                                                                                                                                                                                                                              |

**EsImage** 
------------

“\$type”: “EsImage”

**Properties**

EsImage is comprised of a Base64 encoded byte array (image file).

| Name   | Type    | Description                                                     |
|--------|---------|-----------------------------------------------------------------|
| Base64 | string  | This is the image file encoded in Base64 format.                |
| Width  | Integer | This sizes the image which is being inserted into the document. |

**Inherited Properties**

| EsSyleElement Properties |         |                                                                                                               |
|--------------------------|---------|---------------------------------------------------------------------------------------------------------------|
| Name                     | Type    | Description                                                                                                   |
| BackGroundColor          | String  | The attribute receives the wished color in string format.                                                     |
| Bold                     | Bool    | When set makes the font bold                                                                                  |
| Borders                  | String  | Is a by comma separated string value. “Left, Right” will result in a border left and right of the text object |
| CharSpacing              | Float   | This allows to modify the finite space between two consecutive letters.                                       |
| Color                    | String  | The attribute receives the wished color in string format.                                                     |
| FontName                 | String  | Font Styling from wished font family                                                                          |
| FontSize                 | Integer | An integer value which represents the Font Size                                                               |
| HorizontalAlign          | String  | Like TextAlign this attribute is set to align the element on a Document                                       |
| Italic                   | Bool    | This is set to true or false in invoke the italic property                                                    |
| Opacity                  | Float   | This is a value between 0 and 1 and dictates the visual intensity of an element.                              |
| StrokeColor              | String  | Changes the current color for stroking paths                                                                  |
| TextAlign                | String  | This can either be set to left, right or center using the afore mentioned in string format.                   |
| UnderLine                | Bool    | Set to true the text then is underlined otherwise not.                                                        |
| BorderStyle              | String  | This string must be set to either Solid, Dotted or Dashed the default is Solid.                               |
| WordSpacing              | Float   | This value sets the spacing between the words in a content attribute.                                         |

**EsLine**
----------

“\$type”: “EsLine”

**Properties**

This is a line separator which can serve as a visual que between elements.

| Name         | Type   | Description                                                                                                        |
|--------------|--------|--------------------------------------------------------------------------------------------------------------------|
| LineThikness | float  | A float number that                                                                                                |
| LineType     | String | This string defines one of three line types that can be applied to EsLine element. Solid (Default), Dotted, Dashed |

**Inherited Properties**

| ESBlockElement   |                 |                                                                                                                                                                                                                                                                                                                                                                                              |
|------------------|-----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Name             | Type            | Description                                                                                                                                                                                                                                                                                                                                                                                  |
| HeightPerc       | Float           | When this value is set it can magnify or reduce the height of and element by a percentage value.                                                                                                                                                                                                                                                                                             |
| HeightPoint      | Float           | When this value is set it can magnify or reduce the height of and element by a percentage value.                                                                                                                                                                                                                                                                                             |
| KeepTogether     | Bool            | Will force to keep elements as close as possible to one another.                                                                                                                                                                                                                                                                                                                             |
| KeepTogetherNext | Bool            | The start of the next sibling of this element should be placed in the same area.                                                                                                                                                                                                                                                                                                             |
| Margins          | EsMargins       | Sets the margins around the element to a series of new widths.                                                                                                                                                                                                                                                                                                                               |
| MaxHeight        | Float           | This constrains the height variable to not exceed provided value.                                                                                                                                                                                                                                                                                                                            |
| MinHeight        | Float           | This constrains the height variable to not drop below provided value.                                                                                                                                                                                                                                                                                                                        |
| Padding          | EsPadding       | This allows to set padding for all four sides of an element. Further more one must only set All, or if alternating sides are required set the all property and then the value of the side which is supposed to be different.                                                                                                                                                                 |
| SpacingRatio     | Float           | Sets a ratio which determines in which proportion will word spacing and character spacing be applied when horizontal alignment is justified. This is a value between 0 and 1 and determines how the free space is to be dispersed between char spacing and word spacing. 1 means no additional spacing char spacing will be applied and o means no additional word spacing will be assigned. |
| VerticalAlign    | EsVerticalAlign | Sets the vertical alignment of the element                                                                                                                                                                                                                                                                                                                                                   |
| WidthPerc        | Float           | When this value is set it can magnify or reduce the width of and element by a percentage value.                                                                                                                                                                                                                                                                                              |
| WidthPoint       | Float           | When this value is set it can magnify or reduce the width of and element by a percentage value.                                                                                                                                                                                                                                                                                              |

**EsList** 
-----------

“\$type”: “EsList”

**Properties**

This is a bulleted list of elements as in standard documents. The default Bullet
point is a circle and at the moment cannot be changed.

| Name      | Type                 | Description               |
|-----------|----------------------|---------------------------|
| ListItems | List of EsListItem’s | See EsText for attributes |

### EsListItem 

| Name     | Type                | Description                      |
|----------|---------------------|----------------------------------|
| Elements | List of IEsElements | The attributes are all inherited |

**Inherited Properties**

| ESBlockElement   |                 |                                                                                                                                                                                                                                                                                                                                                                                              |
|------------------|-----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Name             | Type            | Description                                                                                                                                                                                                                                                                                                                                                                                  |
| HeightPerc       | Float           | When this value is set it can magnify or reduce the height of and element by a percentage value.                                                                                                                                                                                                                                                                                             |
| HeightPoint      | Float           | When this value is set it can magnify or reduce the height of and element by a percentage value.                                                                                                                                                                                                                                                                                             |
| KeepTogether     | Bool            | Will force to keep elements as close as possible to one another.                                                                                                                                                                                                                                                                                                                             |
| KeepTogetherNext | Bool            | The start of the next sibling of this element should be placed in the same area.                                                                                                                                                                                                                                                                                                             |
| Margins          | EsMargins       | Sets the margins around the element to a series of new widths.                                                                                                                                                                                                                                                                                                                               |
| MaxHeight        | Float           | This constrains the height variable to not exceed provided value.                                                                                                                                                                                                                                                                                                                            |
| MinHeight        | Float           | This constrains the height variable to not drop below provided value.                                                                                                                                                                                                                                                                                                                        |
| Padding          | EsPadding       | This allows to set padding for all four sides of an element. Further more one must only set All, or if alternating sides are required set the all property and then the value of the side which is supposed to be different.                                                                                                                                                                 |
| SpacingRatio     | Float           | Sets a ratio which determines in which proportion will word spacing and character spacing be applied when horizontal alignment is justified. This is a value between 0 and 1 and determines how the free space is to be dispersed between char spacing and word spacing. 1 means no additional spacing char spacing will be applied and o means no additional word spacing will be assigned. |
| VerticalAlign    | EsVerticalAlign | Sets the vertical alignment of the element                                                                                                                                                                                                                                                                                                                                                   |
| WidthPerc        | Float           | When this value is set it can magnify or reduce the width of and element by a percentage value.                                                                                                                                                                                                                                                                                              |
| WidthPoint       | Float           | When this value is set it can magnify or reduce the width of and element by a percentage value.                                                                                                                                                                                                                                                                                              |

**EsTable** 
------------

“\$type”: “EsTable”

**Properties**

The Table is an element which contains EsTableCell which in return contain the
content one wishes to display. The table can have multiple columns and rows. The
Table has mandatory values such as the column count and the Cell element in its
bare minimum it needs the HeaderCell defined in order to function properly
otherwise you will receive a runtime error.

| Name               | Type               | Description                                                                                                                                                                                                                                                                                                                                                                                  |
|--------------------|--------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ColumnsWidthPoints | List of Floats     | This number dictates how the columns are distributed between the created columns in points.                                                                                                                                                                                                                                                                                                  |
| ColumnsWidthPercs  | List of Floats     | This number dictates how the columns are distributed between the created columns in percent.                                                                                                                                                                                                                                                                                                 |
| HeaderCells        | List of EsTablCell | See EsTablCellfor attributes                                                                                                                                                                                                                                                                                                                                                                 |
| BodyCells          | List of EsTablCell | See EsTablCellfor attributes                                                                                                                                                                                                                                                                                                                                                                 |
| FooterCells        | List of EsTablCell | See EsTablCellfor attributes                                                                                                                                                                                                                                                                                                                                                                 |
| ESBlockElement     |                    |                                                                                                                                                                                                                                                                                                                                                                                              |
| Name               | Type               | Description                                                                                                                                                                                                                                                                                                                                                                                  |
| HeightPerc         | Float              | When this value is set it can magnify or reduce the height of and element by a percentage value.                                                                                                                                                                                                                                                                                             |
| HeightPoint        | Float              | When this value is set it can magnify or reduce the height of and element by a percentage value.                                                                                                                                                                                                                                                                                             |
| KeepTogether       | Bool               | Will force to keep elements as close as possible to one another.                                                                                                                                                                                                                                                                                                                             |
| KeepTogetherNext   | Bool               | The start of the next sibling of this element should be placed in the same area.                                                                                                                                                                                                                                                                                                             |
| Margins            | EsMargins          | Sets the margins around the element to a series of new widths.                                                                                                                                                                                                                                                                                                                               |
| MaxHeight          | Float              | This constrains the height variable to not exceed provided value.                                                                                                                                                                                                                                                                                                                            |
| MinHeight          | Float              | This constrains the height variable to not drop below provided value.                                                                                                                                                                                                                                                                                                                        |
| Padding            | EsPadding          | This allows to set padding for all four sides of an element. Further more one must only set All, or if alternating sides are required set the all property and then the value of the side which is supposed to be different.                                                                                                                                                                 |
| SpacingRatio       | Float              | Sets a ratio which determines in which proportion will word spacing and character spacing be applied when horizontal alignment is justified. This is a value between 0 and 1 and determines how the free space is to be dispersed between char spacing and word spacing. 1 means no additional spacing char spacing will be applied and o means no additional word spacing will be assigned. |
| VerticalAlign      | EsVerticalAlign    | Sets the vertical alignment of the element                                                                                                                                                                                                                                                                                                                                                   |
| WidthPerc          | Float              | When this value is set it can magnify or reduce the width of and element by a percentage value.                                                                                                                                                                                                                                                                                              |
| WidthPoint         | Float              | When this value is set it can magnify or reduce the width of and element by a percentage value.                                                                                                                                                                                                                                                                                              |

### EsTableCell

“HeadderCells”, “BodyCells”, “FooterCells”

**IS MADITORY**

| Name     | Type                | Description                                                                                                                                                               |
|----------|---------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Elements | List of IEsElements | Is able to apply all attributes to the containing elements                                                                                                                |
| RowSpan  | Int                 | This indicates how the column is to be dispersed. [30,55,25] this syntax indicates that there are three rows with each value depicting the inclusive percent width value. |
| ColSpan  | Int                 | This value would be an arbitrary number representing the number of columns.                                                                                               |

**Inherited Properties**

| EsSyleElement Properties |         |                                                                                                               |
|--------------------------|---------|---------------------------------------------------------------------------------------------------------------|
| Name                     | Type    | Description                                                                                                   |
| BackGroundColor          | String  | The attribute receives the wished color in string format.                                                     |
| Bold                     | Bool    | When set makes the font bold                                                                                  |
| Borders                  | String  | Is a by comma separated string value. “Left, Right” will result in a border left and right of the text object |
| CharSpacing              | Float   | This allows to modify the finite space between two consecutive letters.                                       |
| Color                    | String  | The attribute receives the wished color in string format.                                                     |
| FontName                 | String  | Font Styling from wished font family                                                                          |
| FontSize                 | Integer | An integer value which represents the Font Size                                                               |
| HorizontalAlign          | String  | Like TextAlign this attribute is set to align the element on a Document                                       |
| Italic                   | Bool    | This is set to true or false in invoke the italic property                                                    |
| Opacity                  | Float   | This is a value between 0 and 1 and dictates the visual intensity of an element.                              |
| StrokeColor              | String  | Changes the current color for stroking paths                                                                  |
| TextAlign                | String  | This can either be set to left, right or center using the afore mentioned in string format.                   |
| UnderLine                | Bool    | Set to true the text then is underlined otherwise not.                                                        |
| BorderStyle              | String  | This string must be set to either Solid, Dotted or Dashed the default is Solid.                               |
| WordSpacing              | Float   | This value sets the spacing between the words in a content attribute.                                         |

**EsAreaBreak** 
----------------

“\$type”: “BreakType”

**Properties**

This allows you to set a page break. This also allows to progress to the last
page of a document or into the next area of an element.

| Name      | Type   | Description                                                                              |
|-----------|--------|------------------------------------------------------------------------------------------|
| BreakType | String | This has three different page break types the default is NEXT_PAGE ,NEXT_AREA, LAST_PAGE |

**Document Sample**
===================

Everything depicted in {{writing }} is going to be a list of options. Thus if
you see {{yes, no}} the proper way to implement “Configuration”: “yes” or "no"
“Configuration” : “no”
```json
{
   "EmbedFonts": false,
   "PageSize": "LETTER",
   "Header": {
      "Elements": [
         { 
            "$type": "EsText",
            "Content": "This is my page Header",
            "FontSize": 20,
            "TextAlign": "Center"
         },
         {
            "$type": "EsText",
            "Content": "Page {{PageNumber}} #",
            "FontSize": 10,
            "TextAlign": "Right"
         }
      ]
   },
   "PageMargins": {
      "Top": 5,
      "Bottom": 20,
      "Left": 30,
      "Right": 30
   },
   "Sections": [
      {
         "Elements": [
            {
               "$type": "EsImage",
               "Base64": iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAQAAAAnOwc2AAAAY0lEQVQIW2P4z4CADB4MOWAaRVAISExDEwRyfwIJD1QhUTC1DlWwAkzdQBZSZPgAZlxFCBkzPIcyZzIwFDFUM3Qx7IXL/mRQZ2BQYXgIFwAJhYOdBHRdF8M9IOMVwxwGXZAMAPlPVNz/JVQCAAAAAElFTkSuQmCC,
               "Width": 80,
               "HorizontalAlign": "Center"
            },
            { 
               "$type": "EsParagraph",
               "Texts": [],
               "Opacity": 1
            },
            { 
               "$type": "EsText",
               "Content": "Unparalleled Property Services",
               "FontName": "Poor Richard",
               "FontSize": 9,
               "Bold": true,
               "Italic": true,
               "UnderLine": false,
               "CharSpacing": 1,
               "WordSpacing": 2.2,
               "Opacity": 1,
               "BackGroundColor": "white",
               "TextAlign": "Center"
            },
            { 
               "$type": "EsLine",
               "TextAlign": "Center"
            },
            { 
               "$type": "EsText",
               "Content": "Questionnaire",
               "FontName": "Arial",
               "FontSize": 12,
               "Bold": false,
               "Italic": true,
               "UnderLine": false,
               "CharSpacing": 0.5,
               "Opacity": 1,
               "Color": "Black",
               "TextAlign": "Right",
               "Borders": [ 
                  { 
                     "BorderStyle": "Solid"
                  }
               ]
            },
            { 
               "$type": "EsLine",
               "LineType": "solid",
               "Color": "black",
               "HorizontalAlign": "Center"
            },
            { 
               "$type": "EsText",
               "Content": "The Township Community Mastre Association, Inc.",
               "FontName": "Verdana",
               "FontSize": 12,
               "Bold": true,
               "Italic": true,
               "UnderLine": false,
               "Opacity": 1,
               "Color": "black",
               "TextAlign": "Center",
               "Borders": [ 
                  { 
                     "BorderStyle": "Solid"
                  }
               ]
            },
            { 
               "$type": "EsText",
               "Content": "\"Proudly managed by Castle Management LLC.\"",
               "FontName": "Verdana",
               "FontSize": 11,
               "UnderLine": false,
               "Opacity": 1,
               "TextAlign": "Center",
               "Borders": [ 
                  { 
                     "BorderStyle": "Solid"
                  }
               ]
            },
            { 
               "$type": "EsLine",
               "LineType": "solid",
               "WidthPerc": 0,
               "Color": "black",
               "HorizontalAlign": "Center"
            },
            { 
               "$type": "EsTable",
               "ColumnsWidthPoints": [],
               "ColumnsWidthPercs": [ 
                  75,
                  35
               ],
               "BodyCells": [ 
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Owner(s):",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Lois Downie",
                           "Opacity": 1,
                           "TextAlign": "Right",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Buyer(s)",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "KelyC",
                           "Opacity": 1,
                           "TextAlign": "Right",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Address",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "BorderStyle": "Solid"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "2791 Carambola Circle South",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  }
               ],
               "HeaderCells": [ 
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "The certificate has been prepared",
                           "Opacity": 1,
                           "TextAlign": "Left",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Thu, Sep 6, 2018",
                           "Opacity": 1,
                           "TextAlign": "Right",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  }
               ],
               "FooterCells": [],
               "HorizontalAlign": "Right"
            },
            { 
               "$type": "EsTable",
               "ColumnsWidthPoints": [],
               "ColumnsWidthPercs": [ 
                  73,
                  27
               ],
               "BodyCells": [ 
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Color": "red",
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Is there a right of first refusal provided to the members or the association? (Ifyes, see note for more information):",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "No",
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "bottom",
                           "BorderStyle": "Solid",
                           "Color": "lightgray"
                        }
                     ]
                  }
               ],
               "HeaderCells": [ 
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Question",
                           "UnderLine": true,
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  },
                  { 
                     "Elements": [ 
                        { 
                           "$type": "EsText",
                           "Content": "Answer",
                           "UnderLine": true,
                           "Opacity": 1,
                           "Borders": [ 
                              { 
                                 "BorderStyle": "Solid"
                              }
                           ]
                        }
                     ],
                     "Borders": [ 
                        { 
                           "Sides": "none",
                           "BorderStyle": "Solid"
                        }
                     ]
                  }
               ],
               "FooterCells": []
            }
         ]
      }
   ]
}


```
