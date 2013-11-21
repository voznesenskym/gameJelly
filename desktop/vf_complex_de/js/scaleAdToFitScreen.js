	


	function updateAdSize(wrapperDivId){
		var page_scale;	
		var windowWidth = document.documentElement.clientWidth;
		if (windowWidth < 768){
			page_scale = windowWidth / 768;
		} else {
			page_scale = 1;
		}

		var css = "";
		css += ' transform-origin: 0% 0%;';
		css += ' -ms-transform-origin: 0% 0%;';
		css += ' -webkit-transform-origin: 0% 0%;';
		css += ' -moz-transform-origin: 0% 0%;';
		css += ' -o-transform-origin: 0% 0%;',
		css += ' transform: scale('+page_scale+','+page_scale+');';
		css += ' -ms-transform: scale('+page_scale+','+page_scale+');';
		css += ' -webkit-transform: scale('+page_scale+','+page_scale+');';
		css += ' -o-transform: scale('+page_scale+','+page_scale+');';
		css += ' -moz-transform: scale('+page_scale+','+page_scale+');';

		addCssClass('#' + wrapperDivId, css);
	}


	function addCssClass ( selector, styles ){
        try {
            style = document.getElementById('myCustomStyleID');
            temp = style.innerHTML;
            style.innerHTML = temp + selector + "{ " + styles + "}\n";
        } catch (err) {
            style = document.createElement("style");
            style.id = 'myCustomStyleID'
            style.setAttribute('type', 'text/css');
            style.innerHTML = selector + "{ " + styles + " }\n";
            document.head.insertBefore(style,document.head.childNodes[0]);   
        }
    }