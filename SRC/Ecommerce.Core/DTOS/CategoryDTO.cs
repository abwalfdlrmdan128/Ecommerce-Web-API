using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOS
{
    public record CategoryDTO(string Name,string Description);
    public record UpdateCategoryDTO(string Name, string Description,int id);

}
/*
import {
  AfterViewInit,
  Component,
  ElementRef,
  ViewChild
} from '@angular/core';

declare const google: any;

@Component({
  selector: 'app-google-login',
  standalone: true,
  template: `
    <div #googleButton></div>
  `
})
export class GoogleLoginComponent implements AfterViewInit {
  @ViewChild('googleButton', { static: true })
  googleButton!: ElementRef<HTMLDivElement>;

  private readonly clientId =
    'YOUR_GOOGLE_CLIENT_ID';

  ngAfterViewInit(): void {
    if (typeof google === 'undefined') {
      console.error('Google Identity Services did not load.');
      return;
    }

    google.accounts.id.initialize({
      client_id: this.clientId,
      callback: (response: any) => {
        this.handleGoogleResponse(response);
      }
    });

    google.accounts.id.renderButton(
      this.googleButton.nativeElement,
      {
        theme: 'outline',
        size: 'large',
        text: 'signin_with',
        shape: 'rectangular',
        width: 280
      }
    );
  }

  private handleGoogleResponse(response: any): void {
    const idToken = response.credential;

    if (!idToken) {
      console.error('Google ID Token was not returned.');
      return;
    }

    // We'll send this token to our API
    // after implementing the Google login endpoint.
    console.log('Google ID Token received successfully.');
  }
}
*/