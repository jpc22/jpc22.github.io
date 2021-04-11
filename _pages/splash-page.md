---
title: "Refixture"
layout: splash
permalink: /
date: 2016-03-23T11:48:41-04:00
header:
  overlay_color: "#000"
  overlay_filter: "0.5"
  overlay_image: /assets/images/unsplash-image-1.jpg
  actions:
    - label: "Download"
      url: "https://github.com/mmistakes/minimal-mistakes/"
  caption: "Photo credit: [**Unsplash**](https://unsplash.com)"
excerpt: "Refixture helps you arrange your furniture with the help of genetic algorithms."
intro: 
  - excerpt: 'We sometimes find difficulty when attempting to fit everything we want in a limited space. In some cases, rearranging can result in wasted effort through trial and error. Although keeping a list of measurements can help, Refixture allows you to use those measurements to model a virtual room that you can modify freely.'
feature_row:
  - image_path: assets/images/unsplash-gallery-image-1-th.jpg
    alt: "placeholder image 1"
    title: "Define"
    excerpt: "Visualize a scale model of your room by applying real-world measurements to a large variety of furniture."
  - image_path: /assets/images/unsplash-gallery-image-2-th.jpg
    image_caption: "Image courtesy of [Unsplash](https://unsplash.com/)"
    alt: "placeholder image 2"
    title: "Arrange"
    excerpt: "Accurately model your room by interactively moving furniture and saving the result."
    url: "#test-link"
    btn_label: "Read More"
    btn_class: "btn--primary"
  - image_path: /assets/images/unsplash-gallery-image-3-th.jpg
    title: "Generate"
    excerpt: "Unsure about what should go where? Refixture can take a list of your furniture and attempt to come up with a solution in real time."
feature_row2:
  - image_path: /assets/images/unsplash-gallery-image-2-th.jpg
    alt: "placeholder image 2"
    title: "Who can benefit from Refixture?"
    excerpt: 'People adding new life to a room, people moving into a new home, people looking for procedurally generated content.'
    url: "#test-link"
    btn_label: "Read More"
    btn_class: "btn--primary"
feature_row3:
  - image_path: /assets/images/unsplash-gallery-image-2-th.jpg
    alt: "placeholder image 2"
    title: "Placeholder Image Right Aligned"
    excerpt: 'This is some sample content that goes here with **Markdown** formatting. Right aligned with `type="right"`'
    url: "#test-link"
    btn_label: "Read More"
    btn_class: "btn--primary"
feature_row4:
  - image_path: /assets/images/unsplash-gallery-image-2-th.jpg
    alt: "placeholder image 2"
    title: "Placeholder Image Center Aligned"
    excerpt: 'This is some sample content that goes here with **Markdown** formatting. Centered with `type="center"`'
    url: "#test-link"
    btn_label: "Read More"
    btn_class: "btn--primary"
---

{% include feature_row id="intro" type="center" %}

{% include feature_row %}

{% include feature_row id="feature_row2" type="left" %}

{% include feature_row id="feature_row3" type="right" %}

{% include feature_row id="feature_row4" type="center" %}