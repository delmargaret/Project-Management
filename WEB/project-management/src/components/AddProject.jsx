import React, { Component } from 'react';

class ProjectList extends Component{
    render(){ 
        return <div>
                    <table>
                        <tbody>
                        {
                    this.props.proj.map((project) => {  
                        var data = JSON.parse(project); 
                        var id = data.Id;  
                        return <tr key={id}>
                            <td>{data.ProjectName}</td>
                            <td>{data.ProjectDescription}</td>
                            <td>{data.ProjectStartDate}</td>
                            <td>{data.ProjectEndDate}</td>
                            <td>{data.ProjectStatusId}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                    </table>
                </div>
            }
}

class AddProjectForm extends Component{
 
    constructor(props){
        super(props);
        this.state = {name: "", description: "", startDate: "",
        endDate: "", projects: [], nameIsValid: true, descriptionIsValid: true, 
        startDateIsValid: true, endDateIsValid: true};
 
        this.onSubmit = this.onSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
        this.onDescriptionChange = this.onDescriptionChange.bind(this);
        this.onStartDateChange = this.onStartDateChange.bind(this);
        this.onEndDateChange = this.onEndDateChange.bind(this);
        this.loadProjects = this.loadProjects.bind(this);
    }
    validateName(name){
        return name.length>=2;
    }
    validateDescription(description){
        return description.length>10 && description.length<1024;
    }
    //validateEndDate(startDate, endDate){
        //endDate>startDate
        //return endDate>Date.now();
    //}
    onNameChange(e) {
        var val = e.target.value;
        this.setState({name: val});
    }
    onDescriptionChange(e) {
        var val = e.target.value;
        this.setState({description: val});
    }
    onStartDateChange(e) {
        var val = e.target.value;
        this.setState({startDate: val});
    }
    onEndDateChange(e) {
        var val = e.target.value; 
        this.setState({endDate: val});
    }
    loadProjects() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "http://localhost:12124/api/Project/GetProjects", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ projects: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadProjects();
    }
    onProjectSubmit(project) {
        if (project) {
 
            var data = JSON.stringify({"ProjectName":project.name, "ProjectDescription":project.description,
        "ProjectStartDate": project.startDate, "ProjectEndDate": project.endDate});
             console.log(data);
            fetch("http://localhost:12124/api/Project/CreateProject", {
                method: 'POST', 
                body: data,
                type: "json",
                headers: {"Content-Type": "application/json"}
            });
        }
        this.loadProjects();
    }
    onSubmit(e) {
        e.preventDefault();
        var name = this.state.name.trim();
        var description = this.state.description.trim();
        var startDate = this.state.startDate;
        var endDate = this.state.endDate;

        var nameValid = this.validateName(name);
        this.setState({nameIsValid: nameValid});
        var descriptionValid = this.validateDescription(description);
        this.setState({descriptionIsValid: descriptionValid});
        //var endDateValid = this.validateEndDate(startDate, endDate);
        //this.setState({endDateIsValid: endDateValid});
        if (!name || !description || !startDate || !endDate) {
            return;
        }
        if(this.state.nameIsValid===true && this.state.descriptionIsValid===true &&
            this.state.startDateIsValid===true && this.state.endDateIsValid===true){
                this.onProjectSubmit({ name: name, description: description,
                    startDate: startDate, endDate: endDate});
                    this.setState({name: "", description: "", startDate: "",
                    endDate: ""});
            }
    }
    render() {
        var nameError = this.state.nameIsValid===true?"":"* Введите имя";
        var descriptionError = this.state.descriptionIsValid===true?"":" * Неверная длина описания";
        var startDateError = this.state.startDateIsValid===true?"":" * Неверная дата начала проекта";
        var endDateError = this.state.endDateIsValid===true?"":" * Неверная дата окончания проекта";
        if(this.state.projects.length!==0){
            
        }
        return (
            <div>
                <h2>Добавить проект</h2>
                <form onSubmit={this.onSubmit}>
              <p>
                  <input type="text"
                         placeholder="Название проекта"
                         value={this.state.name}
                         onChange={this.onNameChange} />
                    <label>{nameError}</label>
              </p>
              <p>
                  <textarea placeholder="Описание проекта"
                            value={this.state.description}
                            onChange={this.onDescriptionChange} />
                    <label>{descriptionError}</label>    
              </p>
              <p>
                  <input type="date"
                         placeholder="Дата начала проекта"
                         value={this.state.startDate}
                         onChange={this.onStartDateChange} />
                    <label>{startDateError}</label>
              </p>
              <p>
                  <input type="date"
                         placeholder="Дата окончания проекта"
                         value={this.state.endDate}
                         onChange={this.onEndDateChange} />
                    <label>{endDateError}</label>
              </p>
            <input type="submit" value="Добавить" />
          </form>
          <ProjectList proj={this.state.projects}/>
            </div>
          
        );
    }
}

export default AddProjectForm;