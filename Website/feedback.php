<?php
	if(isset($_POST['email'])) {
	 
	    $email_to = "me@aaronwalwyn.com";
	    $email_subject = "Defend the Rocket Feedback";
	 
	    $name = $_POST['name']; // required
	    $email_from = $_POST['email']; // required
	    $platform = $_POST['platform']; // required
	    $feedback = $_POST['feedback']; // required
	 
	    $email_message = "Form details below.\n\n";
	 
	    function clean_string($string) {
	      $bad = array("content-type","bcc:","to:","cc:","href");
	      return str_replace($bad,"",$string);
	    }

	    $email_message .= "Name: ".clean_string($name)."\n"; 
	    $email_message .= "Email: ".clean_string($email_from)."\n";
	    $email_message .= "Platform: ".clean_string($platform)."\n";
	    $email_message .= "Feedback: ".clean_string($feedback)."\n";
	  
	$headers = 'From: '.$email_from."\r\n".
	'Reply-To: '.$email_from."\r\n" .
	'X-Mailer: PHP/' . phpversion();
	 
	@mail($email_to, $email_subject, $email_message, $headers);  
	 
} ?>