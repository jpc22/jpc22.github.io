---
title: "Refixture"
layout: splash
permalink: /
date: 2016-03-23T11:48:41-04:00
header:
  overlay_color: "#000"
  overlay_filter: "0.5"
  overlay_image: /assets/images/Unity_2021-04-20_13-24-28.png
  actions:
    - label: "Try It Now"
      url: "/releases/"
  #caption: "Photo credit: [**Unsplash**](https://unsplash.com)"
excerpt: "Refixture helps you arrange your furniture with the help of genetic algorithms."
intro: 
  - title: "About Refixture"
  - excerpt: 'We sometimes find difficulty when attempting to fit everything we want in a limited space. In some cases, rearranging can result in wasted effort through trial and error. Although keeping a list of measurements can help, Refixture allows you to use those measurements to model a virtual room that you can modify freely.'
feature_row:
  - image_path: assets/images/site/Refixture_2021-04-22_00-49-34.png
    title: "Define"
    excerpt: "Visualize a scale model of your room by applying real-world measurements to a large variety of furniture."
  - image_path: /assets/images/site/ApplicationFrameHost_2021-04-22_00-46-49.png
    #image_caption: "Image courtesy of [Unsplash](https://unsplash.com/)"
    title: "Arrange"
    excerpt: "Accurately model your room by interactively moving furniture and saving the result."
    #url: "#test-link"
    #btn_label: "View Demonstration"
    #btn_class: "btn--primary"
  - image_path: /assets/images/site/Refixture_2021-04-22_00-56-45.png
    title: "Generate"
    excerpt: "Unsure about what should go where? Refixture can take a list of your furniture and attempt to come up with a solution in real time."
feature_row2:  
  - image_path: /assets/images/site/Unity_2021-04-20_13-02-44-2.png
    alt: "placeholder image 2"
    title: "Who can benefit from Refixture?"
    excerpt: 'The idea for this project arose due to new indoor lifestyles during the COVID-19 pandemic. Spending time working and relaxing at home encouraged bringing in more anemities at the cost of increased space management. Managing rooms can be helped with planning, but visualizing those plans becomes easy with Refixture. Therefore, Refixture helps users imagine a new shelf, chair, or excercise bike. For those moving into a new space, save a few different arrangements to evaluate layout options. For those knowledgable with Unity, the project can be modified and used freely through the GitHub repository.'
    url: "/about/"
    btn_label: "More About"
    btn_class: "btn--primary"
feature_row3:
  - image_path: /assets/images/site/Timeline.jpg
    alt: "placeholder image 2"
    title: "Project Timeline"
    excerpt: 'This project was developed over a three month period. With little knowledge of Unity and web design.'
    url: "/roadmap/"
    btn_label: "Roadmap"
    btn_class: "btn--primary"
feature_row4:
  - image_path: /assets/images/Refixture-Flowchart.png
    alt: "Flowchart"
    title: "How it works"
    excerpt: 'Users follow the workflow to define, arrange, and generate rooms of their own. Users create a close approximation of their rooms using the preset furniture designs included. The result may not look the same, but by applying measurements they can be scaled to match their real counterparts in size. Refixture works intuitively, but also provides documentation on all features.'
    url: "/documentation/"
    btn_label: "Documentation"
    btn_class: "btn--primary"
feature_row5:
  - image_path: /assets/images/site/chrome_2021-04-21_23-57-52.png
    title: "Testing and Feedback"
    excerpt: 'The project is welcome to feedback for the improvement of features and bug fixing. The testing plan for this projects consists of trying the latest test build and completing a short survey.'
    url: "/testing-plan/"
    btn_label: "Testing Plan"
    btn_class: "btn--primary"
feature_row6:
  - image_path: /assets/images/unsplash-gallery-image-2-th.jpg
    title: "Distribution"
    excerpt: 'The project has been designed to be open source and non-commercial, with the help of code and assets found royalty free on the web. To avoid distributing work of others without their consent, some assets are not available in the repository.'
    url: "https://github.com/jpc22/jpc22.github.io"
    btn_label: "GitHub Repository"
    btn_class: "btn--primary"
feature_row7:
  - image_path: /assets/images/site/Refixture_2021-04-21_19-22-08.png
    title: "Conclusion"
    excerpt: 'Refixture may be highly useful for your next furniture project. With simple yet effective design, it can take the work out of the process of rearranging your room. Test it out in your browser, or try running the source code or desktop program from the GitHub repository.'
    url: "/releases/"
    btn_label: "Try Refixture"
    btn_class: "btn--primary"
---

{% include feature_row id="intro" type="center" %}

{% include feature_row %}

{% include feature_row id="feature_row2" type="center" %}

{% include feature_row id="feature_row3" type="center" %}

{% include feature_row id="feature_row4" type="left" %}

{% include feature_row id="feature_row5" type="right" %}

{% include feature_row id="feature_row6" type="center" %}

{% include feature_row id="feature_row7" type="center" %}
