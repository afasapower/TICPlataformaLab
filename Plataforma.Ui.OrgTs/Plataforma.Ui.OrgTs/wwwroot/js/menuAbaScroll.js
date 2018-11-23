function menuAbaScroll(qualMenu)
{    
    //alert(qualMenu);
    var menuCurrent = $(qualMenu);
    var hidWidth;
    var scrollBarWidths = 80;
    var widthOfList = function () {
        var itemsWidth = 0;
        menuCurrent.find('.lista-menu .nav-item').each(function () {
            var itemWidth = $(this).outerWidth();
            itemsWidth += itemWidth;
        });
        return itemsWidth;
    };

    var getLeftPosi = function () {
        return menuCurrent.find('.lista-menu').position().left;
    };

    var widthOfHidden = function () {
        return ((menuCurrent.find('.involucro-menu').outerWidth()) - widthOfList() - getLeftPosi()) - scrollBarWidths;
    };

    var reAdjust = function () {
        ((menuCurrent.find('.involucro-menu').outerWidth()) < widthOfList()) ? menuCurrent.find('.scroller-right').show() : menuCurrent.find('.scroller-right').hide();

        if (getLeftPosi() < 0) {
            menuCurrent.find('.scroller-left').show();
        }
        else {
            menuCurrent.find('.item').stop().animate({ left: "-=" + getLeftPosi() + "px" }, 'slow');
            menuCurrent.find('.scroller-left').hide();
        }
    };


    $(window).on('resize', function (e){reAdjust();});
    reAdjust();

    menuCurrent.find('.scroller-right').click(function (e) {
        menuCurrent.find('.scroller-left').fadeIn('slow');
        $(this).fadeOut('slow');
        menuCurrent.find('.lista-menu').stop().animate({ left: "+=" + widthOfHidden() + "px" }, 'slow');
    });

    menuCurrent.find('.scroller-left').click(function (e)
    {
        menuCurrent.find('.scroller-right').fadeIn('slow');
        $(this).fadeOut('slow');
        menuCurrent.find('.lista-menu').stop().animate({ left: "-=" + getLeftPosi() + "px" }, 'slow');        
    });
}