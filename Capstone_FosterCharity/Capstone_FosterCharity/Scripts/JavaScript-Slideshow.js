(function () {

    function Slideshow(element) { //Starts the slide show
        this.el = document.querySelector(element);
        this.init();
    } 

    Slideshow.prototype = { //Tells which slide to go in what order
        init: function () {
            this.wrapper = this.el.querySelector(".slider-wrapper");
            this.slides = this.el.querySelectorAll(".slide");
            this.previous = this.el.querySelector(".slider-previous");
            this.next = this.el.querySelector(".slider-next");
            this.index = 0;
            this.total = this.slides.length;
            this.timer = null;

            this.action();
            this.stopStart();
        },
        _slideTo: function (slide) { //What opacity it is when it fades away
            var currentSlide = this.slides[slide];
            currentSlide.style.opacity = 1;

            for (var i = 0; i < this.slides.length; i++) { //for each loop used to cycle pictures
                var slide = this.slides[i];
                if (slide !== currentSlide) {
                    slide.style.opacity = 0;
                }
            }
        },
        action: function () { //this sets up how fast or slow your slide show is going
            var self = this;
            self.timer = setInterval(function () {
                self.index++;
                if (self.index == self.slides.length) {
                    self.index = 0;
                }
                self._slideTo(self.index);

            }, 5000);  //5 seconds
        },
        stopStart: function () {  //This allows the picture to stop when you mouse over the photo
            var self = this;
            self.el.addEventListener("mouseover", function () {
                clearInterval(self.timer);
                self.timer = null;

            }, false);
            self.el.addEventListener("mouseout", function () {
                self.action();

            }, false);
        }


    };

    document.addEventListener("DOMContentLoaded", function () { //Start the slide over again

        var slider = new Slideshow("#main-slider");

    });

})();

