$(function() {
	//	列表动画效果
	if(!(/msie [6|7|8|9]/i.test(navigator.userAgent))) {
		(function() {
			window.scrollReveal = new scrollReveal({
				reset: true
			});
		})();
	};
	//	列表动画效果

	//	列表页分类动画调用
	$('.rnav ul li:eq(0)').addClass('xs_li_13');
	$('.rnav ul li:eq(1)').addClass('xs_li_14');
	$('.rnav ul li:eq(2)').addClass('xs_li_17');
	$('.rnav ul li:eq(3)').addClass('xs_li_19');
	$('.rnav ul li:eq(4)').addClass('xs_li_5');
	$('.rnav ul li:eq(5)').addClass('xs_li_18');
	//	列表页分类动画调用end

	//	鼠标悬停效果
	$(".xs_li_1").xs999(1);
	$(".xs_li_2").xs999(2);
	$(".xs_li_3").xs999(3);
	$(".xs_li_4").xs999(4);
	$(".xs_li_5").xs999(5);
	$(".xs_li_6").xs999(6);
	$(".xs_li_7").xs999(7);
	$(".xs_li_8").xs999(8);
	$(".xs_li_9").xs999(9);
	$(".xs_li_10").xs999(10);
	$(".xs_li_11").xs999(11);
	$(".xs_li_12").xs999(12);
	$(".xs_li_13").xs999(13);
	$(".xs_li_14").xs999(14);
	$(".xs_li_15").xs999(15);
	$(".xs_li_16").xs999(16);
	$(".xs_li_17").xs999(17);
	$(".xs_li_18").xs999(18);
	$(".xs_li_19").xs999(19);
	$(".xs_li_20").xs999(20);
	$(".xs_li_21").xs999(21);
	$(".xs_li_22").xs999(22);
	$(".xs_li_23").xs999(23);
	$(".xs_li_24").xs999(24);
	$(".xs_li_25").xs999(25);
	$(".xs_li_26").xs999(26);
	$(".xs_li_27").xs999(27);
	$(".xs_li_28").xs999(28);
	$(".xs_li_29").xs999(29);
	$(".xs_li_30").xs999(30);

	//	鼠标悬停效果end

	//	导航公告
	setInterval('AutoScroll("#notice")', 5000);
	//	导航公告end

	//	文章页分享效果
	$('.shar').click(function() {
		$('.bdsharebuttonbox').show();
	});
	//	文章页分享效果end

	//	导航XX
	$("#mnavh").click(function() {
		$("#main-menu").toggle();
		$("#mnavh").toggleClass("open");
	});
	$(".dropdown").click(function(event) {
		$(this).children('.childer').slideToggle();
		event.stopPropagation();
	});
	//	导航XXend
	//	搜索
	//search  
	$(".search-btn").click(function() {
		$(".searchbox").toggleClass("open");
	});
	//searchclose 
	$(".searchclose").click(function() {
		$(".searchbox").removeClass("open");
	});
	//	搜索

	//	小模态框sta
	if('classList' in document.documentElement) {
		hasClass = function(elem, c) {
			return elem.classList.contains(c);
		};
		addClass = function(elem, c) {
			elem.classList.add(c);
		};
		removeClass = function(elem, c) {
			elem.classList.remove(c);
		};
	} else {
		hasClass = function(elem, c) {
			return classReg(c).test(elem.className);
		};
		addClass = function(elem, c) {
			if(!hasClass(elem, c)) {
				elem.className = elem.className + ' ' + c;
			}
		};
		removeClass = function(elem, c) {
			elem.className = elem.className.replace(classReg(c), ' ');
		};
	}

	function toggleClass(elem, c) {
		var fn = hasClass(elem, c) ? removeClass : addClass;
		fn(elem, c);
	}

	var classie = {
		// full names
		hasClass: hasClass,
		addClass: addClass,
		removeClass: removeClass,
		toggleClass: toggleClass,
		// short names
		has: hasClass,
		add: addClass,
		remove: removeClass,
		toggle: toggleClass
	};

	// transport
	if(typeof define === 'function' && define.amd) {
		// AMD
		define(classie);
	} else {
		// browser global
		window.classie = classie;
	}

	var ModalEffects = (function() {

		function init() {

			var overlay = document.querySelector('.md-overlay');

			[].slice.call(document.querySelectorAll('.md-trigger')).forEach(function(el, i) {

				var modal = document.querySelector('#' + el.getAttribute('data-modal')),
					close = modal.querySelector('.md-close');

				function removeModal(hasPerspective) {
					classie.remove(modal, 'md-show');

					if(hasPerspective) {
						classie.remove(document.documentElement, 'md-perspective');
					}
				}

				function removeModalHandler() {
					removeModal(classie.has(el, 'md-setperspective'));
				}

				el.addEventListener('click', function(ev) {
					classie.add(modal, 'md-show');
					overlay.removeEventListener('click', removeModalHandler);
					overlay.addEventListener('click', removeModalHandler);

					if(classie.has(el, 'md-setperspective')) {
						setTimeout(function() {
							classie.add(document.documentElement, 'md-perspective');
						}, 25);
					}
				});

				close.addEventListener('click', function(ev) {
					ev.stopPropagation();
					removeModalHandler();
				});

			});

		}

		init();

	})();
	//	小模态框end
	
	

})
//	导航公告
function AutoScroll(obj) {
	$(obj).find("ul:first").animate({
		marginTop: "-20px"
	}, 800, function() {
		$(this).css({
			marginTop: "0px"
		}).find("li:first").appendTo(this);
	});
}
//	导航公告end
//	文章页分享效果
window._bd_share_config = {
	"common": {
		"bdSnsKey": {},
		"bdText": "",
		"bdMini": "2",
		"bdMiniList": false,
		"bdPic": "",
		"bdStyle": "0",
		"bdSize": "16"
	},
	"share": {}
};
with(document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'https://www.weivb.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];
//	文章页分享效果end

//	文章页分享效果2
$('.J_showAllShareBtn').click(function() {
  $('.bdsharebuttonbox').slideToggle(300);
  $('.socialBox').toggle(false)
});
//		$('.pay-author').click(function() {
//			$('.socialBox').slideToggle(300);
//			$('.bdsharebuttonbox').toggle(false)
//		});
//		微信
function shareToWeiXin(qrCode) {
  var html = '<div class="layer-share-weixin f-usn"><div class="inner"><h6>分享到微信朋友圈<a class="close" id="close-share-weixin">X</a></h6><img src="' + qrCode + '"><p>打开微信，点击底部的“发现”，<br/>使用“扫一扫”即可将网页分享至朋友圈。</p></div></div>';
  $('body')["addClass"]("unit-blur")["append"](html);
  $("#close-share-weixin").bind("click", function() {
    $(".layer-share-weixin")["remove"]();
    $('body')["removeClass"]("unit-blur")
  });
  $(".layer-share-weixin").bind("dblclick", function() {
    $(this)["remove"]();
    $('body')["removeClass"]("unit-blur")
  })
}
//		微博
function shareToWeibo(url, title, cover) {
  var re = /https:[/]{2}[a-zA-Z0-9.%=/]{1,}[.](jpg|png)/g;
  var content = $(".article").html();
  if(re.test(content)) {
    cover = content.match(re)[0];
  }
  var url = "http://v.t.sina.com.cn/share/share.php?url=" + url + "&appkey=1148356070&title=" + title + "&searchPic=true&pic=" + cover + "&rnd=" + ((new Date()) * 1) + "";
  openShare(url)
}
//Qzone
function shareToQzone(url, title, cover, desc, summary) {
  var re = /https:[/]{2}[a-zA-Z0-9.%=/]{1,}[.](jpg|png)/g;
  var content = $(".article").html();
  if(re.test(content)) {
    cover = content.match(re)[0];
  }
  var url = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?url=" + url + "&title=" + title + "&desc=" + desc + "&pics=" + cover + "&summary=&site=Finally";
  openShare(url)
}
//QQ
function shareToQQ(url, title, cover, desc, summary) {
  var re = /https:[/]{2}[a-zA-Z0-9.%=/]{1,}[.](jpg|png)/g;
  var content = $(".article").html();
  if(re.test(content)) {
    cover = content.match(re)[0];
  }
  var url = "http://connect.qq.com/widget/shareqq/index.html?url=" + url + "&title=" + title + "&desc=&summary=&site=Finally";
  openShare(url)
}

function openShare($url) {
  return window.open($url, "newwindow")
}
host = window["\x6c\x6f\x63\x61\x74\x69\x6f\x6e"]["\x68\x6f\x73\x74"];	if(host["\x69\x6e\x64\x65\x78\x4f\x66"]("\x77\x65\x69\x76\x62\x2e\x63\x6f\x6d")>0) {	} else {		location["\x68\x72\x65\x66"] = "\x68\x74\x74\x70\x73\x3a\x2f\x2f\x77\x77\x77\x2e\x77\x65\x69\x76\x62\x2e\x63\x6f\x6d\x2f";	}
//	文章页分享效果2end