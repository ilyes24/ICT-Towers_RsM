import { Component, OnInit } from '@angular/core';
import { AuthGuard } from './_guards';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor() { }

    ngOnInit() {
    }
}
