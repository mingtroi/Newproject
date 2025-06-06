import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import Module from 'module';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));


// main.ts (Angular)
platformBrowserDynamic().bootstrapModule(Module)
  .catch(err => console.error(err));
