<?php
	$con=mysqli_connect("209.17.116.156","aaronwalwynuk","Hitchiker01","aaronwalwyn");
	// Check connection
	if (mysqli_connect_errno())
  	{
		echo "Failed to connect to MySQL: " . mysqli_connect_error();
  	}

	$fName = $_POST["fName"];
	$lName = $_POST["lName"];
	$email = $_POST["email"];
	$platform = $_POST["platform"];

	$sql = "INSERT INTO defend_the_rocket (first_name, last_name, email, platform) VALUES ('$fName', '$lName', '$email', '$platform')";

	if (!mysqli_query($con,$sql))
  	{
  		die('Error: ' . mysqli_error($con));
  	}

?>