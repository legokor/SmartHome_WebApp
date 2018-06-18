import { Component, NgModule, Inject, OnInit, OnDestroy } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable, Subscription } from 'rxjs';
import { MasterUnit } from '../MasterUnit';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { MasterUnitManagerComponent } from '../masterunitmanager/masterunitmanager.component';


@Component({
    selector: 'masterunitupdater',
    templateUrl: './masterunitupdater.component.html'
})
export class MasterUnitUpdaterComponent implements OnInit, OnDestroy {
    private id: string = "";
    private route$: Subscription = new Subscription();
    public model: MasterUnit = new MasterUnit();
    public oldModel: MasterUnit = new MasterUnit();
    public isConflict: boolean = false;
    private baseUrl: string;

    constructor(private route: ActivatedRoute, private http: Http, @Inject('BASE_URL') baseUrl: string, public router: Router) {
        this.baseUrl = baseUrl;
    }

    ngOnInit() {
        this.route$ = this.route.params.subscribe(
            (params: Params) => {
                this.id = params["id"]; // cast to number
            }
        );
        this.readMasterUnit()
    }
    ngOnDestroy() {
        if (this.route$) this.route$.unsubscribe();
    }

    readMasterUnit(): void {
        this.http.get(this.baseUrl + 'api/v1/masterunit/' + this.id)
            .map((response) => response.json())
            .subscribe(
                (data) => {
                    let castedData = data as MasterUnit;
                    this.model = new MasterUnit(castedData.id, castedData.eTag, castedData.customName, castedData.isOn, castedData.ownerId);
                    return;
                },
                error => alert(error)
            );
    }

    onSubmit(): void {
        let userId = sessionStorage.getItem('id');
        if (userId != null) {
            this.model.ownerId = userId;
        }
        let url = this.baseUrl + "api/v1/masterunit";
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = this.model;
        this.http.put(url, body, options)
            .map(res => {
                if (!res.ok) {
                    res.json()
                }
            })
            .subscribe(
                data => this.router.navigate([MasterUnitManagerComponent]),
                error => {
                    this.isConflict = true;
                    this.oldModel = this.model;
                    this.readMasterUnit();
                }
            );
    }

}