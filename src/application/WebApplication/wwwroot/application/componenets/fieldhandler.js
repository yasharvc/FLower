var app = null;
const appName = 'app';
var data = {
	appName: 'This is application Name',
	userName: 'John Doe',
	carousel: false,
	card: false,
	sliders: false,
	slide: 1,
	lorem: 'Lorem ipsum dolor sit amet consectetur, adipisicing elit. Natus, ratione eum minus fuga, quasi dicta facilis corporis magnam, suscipit at quo nostrum!',
	stars: 3,
	slideVol: 39,
	slideAlarm: 56,
	slideVibration: 63
};

function getdata() {
	var res = data;
	res.simpleSuccessDialog = {
		show: false,
		title: 'test',
		msg: "Test"
	};
	res.simpleDangerDialog = {
		show: false,
		title: 'test',
		msg: "Test"
	};
	res.loadingDiv = {
		show: false,
		msg: 'Please wait...'
	};
	return data;
}
var externalMethods = {};
function getMethods() {
	var x= {
		firstpage: function (page) {
			this.showLoading();
			loadPage(page, function () {
				app.showSimpleSuccess("Success", "Page loaded");
				app.hideLoading();
			}, function () {
				debugger;
			}, function () {
				app.showSimpleDanger("Error", "Unauthorized access!");
				app.hideLoading();
			});
		},
		showSimpleSuccess: function (title, msg) {
			this.simpleSuccessDialog.title = title;
			this.simpleSuccessDialog.msg = msg;
			this.simpleSuccessDialog.show = true;
		},
		showSimpleDanger: function (title, msg) {
			this.simpleDangerDialog.title = title;
			this.simpleDangerDialog.msg = msg;
			this.simpleDangerDialog.show = true;
		},
		showLoading: function (msg) {
			this.loadingDiv.show = true;
			this.loadingDiv.msg = msg || 'Please wait...';
		},
		hideLoading: function () {
			this.loadingDiv.show = false;
			this.loadingDiv.msg = '';
		},
	};
	for (var attr in externalMethods) {
		x[attr] = externalMethods[attr];
	}
	return x;
}

var componenetsURLs = [
	{ name: "text" },
	{ name: "integer" },
	{ name: "helloworld" },
	{ name: "simpledialogs", isForApp: true },
];

function uuidv4() {
	return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
		var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
		return v.toString(16);
	});
}

function loadComponenets(index, success) {
	if (index >= componenetsURLs.length)
		success();
	else {
		if (componenetsURLs[index].isForApp) {
			setTimeout(function () { loadComponenets(index + 1, success); }, 100);
		} else {
			addHTMLIntoPublicDiv(componenetsURLs[index].name + '.html', function () {
				setTimeout(function () { loadComponenets(index + 1, success); }, 100);
			});
		}
	}
}

function getScripts(tes) {
	var res = [];
	var start = 0;
	var end = 0;

	while (true) {
		start = tes.indexOf('<script>', end);
		if (start == -1)
			return res;
		start += '<script>'.length
		end = tes.indexOf('</script>', start);
		if (end == -1)
			return res;
		res.push(tes.substring(start, end));
	}
}

function loadPage(page, success, error, unauth) {
	readHTML(page, function (a, content) {
		var scripts = getScripts(content);
		scripts.forEach(function (script) { eval(script); });
		document.getElementById(appName).innerHTML = content;
		createApp(success);
	}, error, unauth);
}

loadComponenets(0, function () {
	var appElm = createAppDiv();
	readHTML('firstpage.html', function (a, content) {
		appElm.innerHTML = content;
		createApp();
	}, null);
});

function createAppDiv() {
	var res = document.createElement('div');
	res.id = appName;
	document.body.appendChild(res);
	return res;
}

function createApp(success) {
	const { formComponent, textComponent } = createComponents();
	loadAppComponents(0, function () {
		app = new Vue({
			el: '#' + appName,
			data: function () {
				return getdata();
			},
			components: {
				'form-component': formComponent,
				'text-component': textComponent
			},
			methods: getMethods()
		});
		if (success)
			success(app);
	});
}

function createComponents() {
	const formComponent = {
		template: '#xyz',
		props: ['title', 'name'],
		data: function () {
			return {
				inputLabel: 'Name'
			};
		}
	};
	const textComponent = {
		template: '#text-cmp-template',
		props: ['title'],
		data: function () {
			return {
				inputLabel: 'Test'
			};
		}
	};
	return { formComponent, textComponent };
}

function loadAppComponents(index, success) {
	if (index >= componenetsURLs.length)
		success();
	else {
		if (componenetsURLs[index].isForApp) {
			addHTMLIntoAppDiv(componenetsURLs[index].name + '.html', function () {
				setTimeout(function () { loadAppComponents(index + 1, success); }, 100);
			});
		} else {
			setTimeout(function () { loadAppComponents(index + 1, success); }, 100);
		}
	}
}