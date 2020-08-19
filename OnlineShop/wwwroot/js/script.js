   $(document).ready(function () {
    $('#wrapper').click(function(){
        $('.icon').toggleClass('close');
        $('nav').toggleClass('active');                
        $('.overlay').toggleClass('active');     
    });

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 400);
        return false;
    });
    
    $(function(){
        $('#myFormSubmit').click(function(e){
          e.preventDefault();
          $('#formResults').text($('#myForm').serialize());        
        });
    });
    
    

    $('.post-wrapper2').slick({
        infinite: true,
        slidesToShow: 5,
        slidesToScroll: 1,
        prevArrow: '.arrow_prev2',
        nextArrow: '.arrow_next2',
        autoplay: true,
        dots: false,              
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1,
                    infinite: true,                        
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    arrows: false,
                    prevArrow: '',
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]           
    })
   });

