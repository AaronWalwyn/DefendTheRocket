$(document).ready(function(){
	console.log("Website Loaded");

	//Lightbox Functions
	$(".thumbnail").click(function(event){
		$(".media-area").html($(event.target).attr("data-media-html"));
	});

	$("#beta-button").click(function(){
		$("article").fadeOut("fast", function(){
			$(".beta-signup").fadeIn("fast", function(){});
		});
	});

	$("#beta-form").submit(function() {
		$.ajax({
        	type: "POST",
           	url: "beta-signup.php",
           	data: $("#beta-form").serialize(), // serializes the form's elements.
           	success: function(data)
           	{
           		console.log("Submitted...");
           	}
        });

		$(".beta-signup").fadeOut("fast", function(){
			$("article").fadeIn("fast", function(){});
		});

    	return false;
	});

	$(".reset-button").click(function(){
		$(".beta-signup").fadeOut("fast", function(){
			$("article").fadeIn("fast", function(){});
		});
	});

	$(".leave-feedback").click(function(){
		$("#feedback-form").fadeIn("slow", function(){});
	});

	$("#feedback-form").submit(function(){
		$.ajax({
			type: "POST",
			url: "feedback.php",
			data: $("#feedback-form").serialize(),
			success: function(data)
			{
				console.log("Feedback submitted...");
			}
		});

		$("#feedback-form").fadeOut("fast", function(){});

		return false;
	});
});