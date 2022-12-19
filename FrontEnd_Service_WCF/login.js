
function handleClick(){
    username = document.getElementById("Username").value;
    password = document.getElementById("Password").value;
    console.log(username);
    console.log(password);
    axios.get("http://localhost:5001/Service1.svc/getUsers/"+username+"/"+password).then(function(res){
      console.log(res);
      if(res.data == ""){
        alert("Sikertelen bejelentkezés!");
      }
      else{
        alert("Sikeres bejelentkezés! Átirányítás folyamatban.");
        document.getElementById("loginbtn").onclick=location.href="bejelentkezve.html";
      }
    })
}

function handleRegister(){
  event.preventDefault();
  const data = 
  {
    Id: 0,
    Active: 0,
    Banned: 0,
    Email: document.getElementById("Email").value,
    Fullname: document.getElementById("FullName").value,
    Password: document.getElementById("Password").value,
    Rank: 0,
    Uname: document.getElementById("Username").value
  }
  if(document.getElementById("Email").value != '' && document.getElementById("FullName").value && '' || document.getElementById("Password").value != '' && document.getElementById("Username").value != '')
  {
    alert("Sikeres regisztráció!");
    location.href="bejelentkezve.html";
  }
  else
  {
      alert("A regisztrációhoz tölsd ki az összes mezőt!");
      return;
  }
  axios.post("http://localhost:5001/Service1.svc/UserPostDB",data).then(function(res){
    console.log(res);
    
      alert(res.data.postUserResult);
  })
}
