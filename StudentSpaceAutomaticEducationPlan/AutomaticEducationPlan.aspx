<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutomaticEducationPlan.aspx.cs" Inherits="StudentSpaceAutomaticEducationPlan.AutomaticEducationPlan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>StudetSpace</title>

  <meta name="description" content="The HTML5 Herald">
  <meta name="author" content="GeetaZaildar">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link href="//db.onlinewebfonts.com/c/b6968516e7add824ac6ed94c7098bd06?family=CentGothWGL" rel="stylesheet" type="text/css"/> 
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  
    <link href="css/bootstrap-grid.min.css" rel="stylesheet" />
    <link href="css/bootstrap-reboot.min.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
  <script type="text/javascript" src="js/bootstrap.bundle.min.js"></script>
  
</head>
<div class="container_fluid ">

<div class="MainMenu">
			 <div class="container">
			 <div class="col-lg-12">
		    	<div class="pos-f-t">
  <div class="collapse" id="navbarToggleExternalContent">
    <div class="menu_inner">
      <ul>
	  <li><a href="#" class="menu_link">Home</a></li>
	  <li><a href="#" class="menu_link">Menu Link</a></li>
	  <li><a href="#" class="menu_link">Menu Link</a></li>
	  <li><a href="#" class="menu_link">Menu Link</a></li>
	  <li><a href="#" class="menu_link">Menu Link</a></li>
	  </ul>
    </div>
  </div>
  </div>
  </div>
   <div class="row"> 
   <div class="col-lg-4 text-left"> 
  <nav class="navbar navbar-dark ">
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
  </nav>
</div>
 <div class="col-lg-4 text-center">
 <a href="#"><img src="images/main_logo.png"></a>
 </div>
  <div class="col-lg-4 text-right header_top_right">
		<p><span>Logged in as :<span><br>
	support admin (AdminStaff)</p>
		<a href="#"><img src="images/logout.png"></a>
  </div>
  </div>
</div> 
				</div>
	<div class="page_title"><h1>Education Plan</h1></div>
				</div>
		<div class="container"> 
		<div class="main_box">
			<div class="orange_bar row">
			<div class="col-md-6 text-left">
			<h2>Carmen Melendez <span class="line_hor"></span></h2> <h2>Education Plan</h2>
			</div>
			<div class="col-md-6 text-right main_right_orange">
			<a href="#"><p>Print Education Plan </p><img src="images/printer.png"></a>
			</div>
			
			</div>
		
		<div class="row">
			<div class="left_main col-md-3 text-center">				
					<a href="#"><img src="images/defaultimage.png">
					Upload Photo
					</a>	
				<ul>
						<li><p><span>ID :</span> 39790</p></li>
						<li><p><span>Name :</span> CARMEN MELENDEZ</p></li>
						<li><p><span>DOB :</span> Aug 12 1978</p></li>
						<li><p><span>Email :</span> support@studentspace.com</p></li>
				<ul>
				
				<div class="shortlinks">
				<h2>Other Info </h2> 
				<hr>
				<ul class="shorlistlinks">
					<li><a href="#"><img src="images/logo1.png">Multiple Address</a></li>
					<li><a href="#"><img src="images/logo2.png">Employment On-Campus</a></li>
					<li><a href="#"><img src="images/logo3.png">Employment Off-Campus</a></li>
					<li><a href="#"><img src="images/logo4.png">High School</a></li>
					<li><a href="#"><img src="images/logo5.png">Time Based Cohort</a></li>
					<li><a href="#"><img src="images/logo6.png">Financial Aid</a></li>
				</ul>
				</div>
			</div>
			<div class="right_main col-md-9"> 
				<div class="edu_plan_box">
				<div class="row">
				<div class="col-md-8">
					<h2>(CTP-MED-BI-D) MEDICAL PLAN INVOICING-BILLING</h2>
					<h2><i>Started on Aug-Dec 2018</i></h2>
					
				</div>
				<div class="col-md-4">
					<h3>In Progress</h3>
				</div>
				</div>
				</div>
				<div class="main_tabs">
				<ul class="nav nav-pills " id="pills-tab" role="tablist">
				  <li class="nav-item">
					<a class="nav-link active" id="pills-Courses-tab" data-toggle="pill" href="#pills-Courses" role="tab" aria-controls="pills-Courses" aria-selected="true">Courses</a>
				  </li>
				  <li class="nav-item">
					<a class="nav-link" id="pills-Schedule-tab" data-toggle="pill" href="#pills-Schedule" role="tab" aria-controls="pills-Schedule" aria-selected="false">Schedule</a>
				  </li>
				  <li class="nav-item">
					<a class="nav-link" id="pills-Requirements-tab" data-toggle="pill" href="#pills-Requirements" role="tab" aria-controls="pills-Requirements" aria-selected="false">Requirements</a>
				  </li>
				</ul>
				
				<div class="tab-content" id="pills-tabContent">				
	<div class="tab-pane fade show active" id="pills-Courses" role="tabpanel" aria-labelledby="pills-Courses-tab">
					  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">+ Add Course</button>
						<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
						  <div class="modal-dialog" role="document">
							<div class="modal-content">
							  <div class="modal-header">
								<h5 class="modal-title" id="exampleModalLabel">Select Course</h5>
								<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								  <span aria-hidden="true">&times;</span>
								</button>
							  </div>
							  <div class="modal-body">
								...
							  </div>
							  <div class="modal-footer">
								<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
								<button type="button" class="btn btn-primary">Save changes</button>
							  </div>
							</div>
						  </div>
						</div>
				<div class="row">
				<div class="col-md-9">
				<div class="bs-example">
								<div class="accordion" id="accordionExample">
									<div class="card">
										<div class="card-header" id="headingOne">
											<h2 class="mb-0">
												<button type="button" class="btn btn-link" data-toggle="collapse" data-target="#collapseOne"><i class="fa fa-plus"></i>2019-2020</button>									
											</h2>
										</div>
										<div id="collapseOne" class="collapse " aria-labelledby="headingOne" data-parent="#accordionExample">
											<div class="card-body">
												<div class="row">
													<div class="col-md-4 subjects_inner">
													<h2>Summer 2019</h2>
														 <ul class="subject" > 
															 
															  <li><p>OFAD 1100</p><span class="close">x</span></li>

															  <li><p>NURS 1201</p>
															 
															  
															  <span class="close">x</span></li>
															  
															 <a href="#">Rebuilt plan here..</a>
															</ul>
														
													</div>
													<div class="col-md-4 subjects_inner">
													<h2>Fall 2019</h2>
														 <ul class="subject" > 
															 
															  <li><p>OFAD 1100</p><span class="close">x</span></li>
															  <li><p>NURS 1201</p><br>
																  <h3>TR 1700-1959</h3><br>
																  <h3>TR 1700-1959</h3><br>
																  <h3 class="caution"><i class="fa fa-exclamation-triangle" aria-hidden="true"></i>Resolve Time Conflict
</h3><br>
																  <span class="close">x</span>
															  </li>
															   <li><p>OFAD 1100</p><span class="close">x</span></li>
															   <a href="#">Rebuilt plan here..</a>
															 
															</ul>
														
													</div>
													<div class="col-md-4 subjects_inner">
													<h2>Spring 2020</h2>
														 <ul class="subject" > 
															 
															  <li><p>FYIS 101</p><span class="close">x</span></li>															  
															   <li><p>MATH 101</p><span class="close">x</span></li>
															   <li><p>ENGL 101</p><span class="close">x</span></li>
															   <li><p>OFAD 1110</p><span class="close">x</span></li>
															   <a href="#">Rebuilt plan here..</a>
															 
															</ul>
														 
													</div>
												</div>
											</div>
										</div>
									</div>
									<div class="card">
										<div class="card-header" id="headingTwo">
											<h2 class="mb-0">
												<button type="button" class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseTwo"><i class="fa fa-plus"></i> 2020-2021</button>
											</h2>
										</div>
										<div id="collapseTwo" class="collapse " aria-labelledby="headingTwo" data-parent="#accordionExample">
											<div class="card-body">
												<div class="row">
													<div class="col-md-4 subjects_inner">
													<h2>Summer 2019</h2>
														 <ul class="subject" > 
															 
															  <li><p>OFAD 1100</p><span class="close">x</span></li>

															  <li><p>NURS 1201</p>
															 
															  
															  <span class="close">x</span></li>
															  
															 <a href="#">Rebuilt plan here..</a>
															</ul>
														
													</div>
													<div class="col-md-4 subjects_inner">
													<h2>Fall 2019</h2>
														 <ul class="subject" > 
															 
															  <li><p>OFAD 1100</p><span class="close">x</span></li>
															  <li><p>NURS 1201</p><br>
																  <h3>TR 1700-1959</h3><br>
																  <h3>TR 1700-1959</h3><br>
																  <h3 class="caution"><i class="fa fa-exclamation-triangle" aria-hidden="true"></i>Resolve Time Conflict</h3><br>
																  <span class="close">x</span>
															  </li>
															   <li><p>OFAD 1100</p><span class="close">x</span></li>
															   <a href="#">Rebuilt plan here..</a>
															 
															</ul>
														
													</div>
													<div class="col-md-4 subjects_inner">
													<h2>Spring 2020</h2>
														 <ul class="subject" > 
															 
															  <li><p>FYIS 101</p><span class="close">x</span></li>															  
															   <li><p>MATH 101</p><span class="close">x</span></li>
															   <li><p>ENGL 101</p><span class="close">x</span></li>
															   <li><p>OFAD 1110</p><span class="close">x</span></li>
															   <a href="#">Rebuilt plan here..</a>
															 
															</ul>
														 
													</div>
												</div>
											</div>
										</div>
									</div>
									<!-- <div class="card"> -->
										<!-- <div class="card-header" id="headingThree"> -->
											<!-- <h2 class="mb-0"> -->
												<!-- <button type="button" class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseThree"><i class="fa fa-plus"></i> Extra Tab</button>                      -->
											<!-- </h2> -->
										<!-- </div> -->
										<!-- <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample"> -->
											<!-- <div class="card-body"> -->
												<!-- <p></p> -->
											<!-- </div> -->
										<!-- </div> -->
									<!-- </div> -->
								</div>
				</div>
				</div>
				<div class="col-md-3 dragger_box">
				  <a href="#"><img class="img-responsive" src="images/dragger.png"></a>
				</div>
				</div>
				<a class="cancel_btn" href="#">Cancel Changes</a>
				<a class="done_btn" href="#">Submit Course Plan</a>
	  </div>
				  <div class="tab-pane fade" id="pills-Schedule" role="tabpanel" aria-labelledby="pills-Schedule-tab">
					 <div class="calender_content">
						  <div class="row">
								  <div class="col-md-6 text-left">
									<h3>Summer 2019</h3> <a href="#">next session ></a>
								  </div>
								   <div class="col-md-6 text-right">
								 <a href="#"><i class="fa fa-calendar" aria-hidden="true"></i> Reschedule a Course</a>
								 </div>
								 <div class="col-md-12 mt-4">
								 <img src="images/cal.jpg">
								 </div>
						 </div>
					 </div>
				  </div>
				  
				  <div class="tab-pane fade" id="pills-Requirements" role="tabpanel" aria-labelledby="pills-Requirements-tab">
					  <div class="requirement_content">
							<div class="row">
									<div class="col-md-6 text-left">
					  
					  
									</div>
									<div class="col-md-6 text-right buttons_top_req">
											
											<div class="dropdown">
											<a type="button" class="orange-btn ">Show all</a>
											  <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
												Sort by
											  </button>
											  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
												<a class="dropdown-item" href="#">Date</a>
												<a class="dropdown-item" href="#">New</a>
												<a class="dropdown-item" href="#">Old</a>
											  </div>
											</div>	
											
									</div>
									<div class="col-md-12 div_scroller">
										<div class="main_course_desc">
											<h2>(CTP-MED-BI-D) MEDICAL PLAN </h2>
											<p>Note: This course required 4.0 minimum grade to Clear.</p>
											<p class="warning">Required Courses(Major Courses)</p>
										</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="main_course_desc pt-3">											
												<p>Note: This course required 4.0 minimum grade to Clear.</p>
												<p class="warning">Required Courses(Major Courses)</p>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
											<div class="course_all_data_box">
												<div class="row">
													<div class="col-md-6 text-left">
													<h2>OFAD 1100</h2>
													<p>Introduction to AFAD</p>
													</div>
													<div class="col-md-2 text-center">
													<p>3 Units</p>												
													</div>
													<div class="col-md-4 text-center">
													<p>Planned for Fall 2020</p>
													</div>
												</div>
											</div>
									</div>
							</div>
					</div>				
			</div>
		 </div> 
		</div>		
	   </div>
	  </div>
	 </div>
	</div>


			<div class="footer text-center">
				<h6>Copyright © 2020.   s t u d e n t | s p a c e  Corporation. All Rights Reserved.</h6>
			</div>
<script>
    $(document).ready(function(){
        // Add minus icon for collapse element which is open by default
        $(".collapse.show").each(function(){
        	$(this).prev(".card-header").find(".fa").addClass("fa-minus").removeClass("fa-plus");
        });
        
        // Toggle plus minus icon on show hide of collapse element
        $(".collapse").on('show.bs.collapse', function(){
        	$(this).prev(".card-header").find(".fa").removeClass("fa-plus").addClass("fa-minus");
        }).on('hide.bs.collapse', function(){
        	$(this).prev(".card-header").find(".fa").removeClass("fa-minus").addClass("fa-plus");
        });
    });
</script>

<script type="text/javascript" src="js/bootstrap.min.js"></script>
   	<script src="js/jquery.min.js"></script>
	
	 
	<script>
var closebtns = document.getElementsByClassName("close");
var i;

for (i = 0; i < closebtns.length; i++) {
  closebtns[i].addEventListener("click", function() {
    this.parentElement.style.display = 'none';
  });
}
</script>

	</footer>

<body>


</html>
