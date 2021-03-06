# Hackathon Submission Entry form

## Team name

⟹ OMeGa

## Category

⟹ The best enhancement to the Sitecore Admin (XP) for Content Editors & Marketers

## Description

⟹ Purpose of the module is to help Sitecore content editors and marketers understand the layout structure of the editing item better.

⟹ While standard Sitecore Presentation Details dialoig shows the item presentation components in one level list structure, that is makes it hard to understand relation between components and placeholders, the Presentation Visualization dialog shows the editing item layout structure using a tree, that allows to see and understand presentation better.

## Video link

⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Video link](https://www.youtube.com/watch?v=POa7mtDc4bE)

## Installation instructions

> -   Set up any topology of clean Sitecore 10.1 using standard Sitecore installation GUI tool, or using `.\Start-Hackathon.ps1` script.
> -   Use the Sitecore Installation wizard to install files/Sitecore-Presentation-Visualizer-1.0.zip [package](files/Sitecore-Presentation-Visualizer-1.0.zip)

## Usage instructions

⟹ The Presentation Visualizer dialog can be opened using the "Visualize" button that is placed on the "Layout" section of the "PRESENTATION" tab of Sitecore Content Editor.
![Visualize button](files/1-VisualizeButton.png?raw=true "Visualize button")

When the button is clicked, the result should be like this (it depends on which item is in context):

![The dialog](files/2-TheDialog.png?raw=true "The dialog")

Cards on the dialog (when item contains any presentation) represents device, placeholders or renderings. It also shows rendering data source info and rendering parameters if specified. Each card is clickable and click on the card opens informatinal pop up, that displays the element display name or rendering parameters (for renderings only).

![Rendering Parameters](files/3-RenderingParameters.png?raw=true "Rendering Parameters")
