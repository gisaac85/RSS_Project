// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetMoreFeeds() {
	
	var title1 = document.getElementById('title1');
	var description1 = document.getElementById('desc1');
	var pd1 = document.getElementById('pd2');
	
	var title2 = document.getElementById('title2');
	var description2 = document.getElementById('desc2');
	var pd2 = document.getElementById('pd2');
   
	var title3 = document.getElementById('title3');
	var description3 = document.getElementById('desc3');
	var pd3 = document.getElementById('pd3');

	$.ajax({
		url: 'home/GetMoreFeeds',
		type: 'json',
		success: function (data) {			
		
				title1.innerText = "Title: "+JSON.stringify(data[0].title);
				description1.innerText = "Description: "+ JSON.stringify(data[0].description);
				pd1.innerText = "Publicationdate: "+ JSON.stringify(data[0].publicationDate);
			    
				title2.innerText = "Title: "+ JSON.stringify(data[1].title);
				description2.innerText = "Description: "+ JSON.stringify(data[1].description);
				pd2.innerText = "Publicationdate: "+ JSON.stringify(data[1].publicationDate);
			
				title3.innerText = "Title: "+ JSON.stringify(data[2].title);
				description3.innerText = "Description: "+ JSON.stringify(data[2].description);
				pd3.innerText = "Publicationdate: "+ JSON.stringify(data[2].publicationDate);			
		},
		error: function (xhr) { 
			alert(xhr.responseText);

		}
	});
}


