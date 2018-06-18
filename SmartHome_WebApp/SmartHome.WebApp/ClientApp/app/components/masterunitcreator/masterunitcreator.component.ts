import { Component, NgModule, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs';
import { MasterUnit } from '../MasterUnit';
import { Router } from '@angular/router';
import { MasterUnitManagerComponent } from '../masterunitmanager/masterunitmanager.component';


@Component({
    selector: 'masterunitcreator',
    templateUrl: './masterunitcreator.component.html'
})
export class MasterUnitCreatorComponenet {
    public model: MasterUnit = new MasterUnit();
    private baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string, public router: Router) {
        this.baseUrl = baseUrl;
    }

    onSubmit(): void {
        let userId = sessionStorage.getItem('id');
        if (userId != null) {
            this.model.ownerId = userId;
        }
        let url = this.baseUrl + "api/v1/masterunit";
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = this.model;//JSON.stringify(this.model);
        this.http.post(url, body, options)
            .map(res => res.json())
            .subscribe(
                data => this.router.navigate([MasterUnitManagerComponent]),
                error => alert(error)
            );
    }

}