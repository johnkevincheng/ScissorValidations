# ScissorValidations
A validations framework automating both client-side and server-side validations.

Validations is an essential part of interactive software development. We can get away without it, but unexpected things may happen when a user starts entering unexpected data. Having said that, validation is an oft neglected part of the development process. It's rarely an interesting part of writing software, and when developer's fatigue starts to creep in, becomes the least prioritized task. The system works with or without it, so lazy developers see it as an optional component, not an essential one. <br />
<br />

To help combat this repetitive task of implementing validations to data entry modules, I've developed this validations framework to automate the configurations required to validate each field. This handles both client-side scripts validation as well as server-side validation. As there are varieties of jQuery-based validation libraries available, the framework has the flexibility to use one's preferred library, or even custom solutions. <br />
<br />

The primary goals of this framework are:
<ol>
<li>to automate some aspects of page validation across an entire project, and</li>
<li>to ensure these validations are uniform, making it easier to make project-wide changes</li>
</ol>

The framework has similarities to .NET's built-in StringValidatorAttribute and other similar attributes, and in fact I thought I had found an out-of-the-box solution to what I wanted, but alas, this attribute is usable only for custom configuration sections. It could not be used for our own custom codes.<br />
<br />

To start, consider a class named <strong>Employee</strong>, containing basic employee information. <br />
<a href="https://gist.github.com/johnkevincheng/294c158b2b24f7d94cb66df9507f0348.js">Link to Gist</a>

For this class, we define validation rules using the <strong>StringValidatorAttribute</strong> (and other corresponding attributes) of the validations framework. To apply size validations to the name fields, we define them as follows: <br />
<a href="https://gist.github.com/johnkevincheng/bcf2c3689cd857916d0ec97625e63b5a.js">Link to Gist</a>

The above definition indicates that the <em>FirstName</em> field should be between 5 to 50 characters in length, inclusive, and is a required field, while the <em>MiddleName</em> field should be between 5 to 20 characters in length, inclusive, but this time is an optional field as the <em>IsRequired</em> property is not set.<br />
<br />

All the validator attributes have the same first constructor argument: the <em>FieldLabel</em>. The <em>FieldLabel</em> is the friendly display name when referring to this property during validation operations. For StringValidatorAttribute, the second and third arguments define the minimum and maximum text lengths. There are other optional settings for the various attributes, like the IsRequired, which defines the property as a required input, among others.<br />
<br />

Below is the entire Employee class with appropriate validator attributes defined.<br />
<a href="https://gist.github.com/johnkevincheng/f16d490aa174c714fc7dc736e6cff8ba.js">Link to Gist</a>

This configures the validation rules to be used for each field in the data entry pages of our application.<br />
<br />

Of note is <strong>IntValidationAttribute</strong>, whose minimum/maximum values are overridden internally as needed, depending on the scope of the Int type decorated. For example, if this attribute is decorating an <strong>Int16</strong> property but was defined with a maximum value more than the maximum value of an <strong>Int16</strong>, internally the validator shall override the defined maximum to instead use <em>Int16.MaxValue</em>.<br />
<br />

On the web page where we receive input for <strong>Employee</strong> details, place textboxes for each of the <strong>Employee</strong> fields. Place a button to trigger the validation on postback. Additionally, we add a Repeater control to display any validation errors found. We use BootStrap framework for the design.<br />
<br />

<div class="separator" style="clear: both; text-align: center;"><a href="https://1.bp.blogspot.com/-aFy9CSs28aw/WVfRg5xMF9I/AAAAAAAAALk/TRSukxcxqSwQhEvfOFbct8SQrjXSfAuUACLcBGAs/s1600/testpage.PNG" imageanchor="1" style="margin-left: 1em; margin-right: 1em;"><img border="0" src="https://1.bp.blogspot.com/-aFy9CSs28aw/WVfRg5xMF9I/AAAAAAAAALk/TRSukxcxqSwQhEvfOFbct8SQrjXSfAuUACLcBGAs/s320/testpage.PNG" width="320" height="201" data-original-width="707" data-original-height="445" /></a></div>

First, let's check the server-side validations. Best practice is to validate on the server no matter how well written our client-side validations are written, as client-side validations is dependent on so many variables. To validate the inputs, we call the function <em>ScissorValidations.Validator.Validate()</em>, which accepts an instance of the <strong>Employee</strong> class (can be a new or existing instance) and the mapping definition <strong>Dictionary&lt;String, Validator.FieldMap&gt;</strong>.<br />
<a href="https://gist.github.com/johnkevincheng/2517d57d6556827f2b796f8f1dbf182c.js">Link to Gist</a>

The fieldMapping variable defines how the class properties map to the page controls. Each element in the map holds the name of the property, and the <strong>FieldMap</strong>, which holds the page control as well as it's value to be validated. The <em>Validate()</em> function returns all validation results found, which can be used as a data source for a repeater control, for example. If none are returned, then it means that the inputs passed validation based on the defined rules, and can now be safely saved.<br />
<br />

As a shortcut, it is also possible to let the <em>Validate()</em> function auto-assign the validated values directly into the data class instance that was passed. This is to minimize the need to assign and perform any casting needed which were already done internally during the validation process. Note that this only passes any values deemed valid by the validation process. To enable this functionality, set the global setting <em>ScissorValidations.Validator.Settings.CopyValuesOnValidate</em> to true. For ASP.NET, this can be set during the ApplicationStart() event in global.asax as below.<br />
<a href="https://gist.github.com/johnkevincheng/cb82521a2b6ec1d89b9cf5d35e7921e7.js">Link to Gist</a>

Now for client-side validations. Scissors Validation Framework acknowledges that client-side validations can have many strategies, be it custom Javascript validations or other established frameworks like BootStrap Validations. Due to this, it is normally advisable to implement one's own implementor, which implements the <strong>IValidationImplementor</strong> interface. For simplicity, let us use a built-in implementor already in the framework named <strong>BootstrapValidationImplementor</strong>, found in the ScissorValidations.ValidationImplementors namespace. This implementor uses simple validation configurations based on the BootStrap Validation library.<br />
<br />

To wire up the page input controls with the required attributes to work with BootStrap Validations, we use the function <em>InitializeClientValidators()</em>, which accepts another fieldMapping variable, but using a slightly different Dictionary definition. Additionally, generic types need to be defined for the data class type containing the validator attributes we need, as well as an implementor class like <strong>BootstrapValidationImplementor</strong>.<br />
<br />
<a href="https://gist.github.com/johnkevincheng/6dc78c916cef417778bc42b357f617d5.js">Link to Gist</a>

Similar to the fieldMappings on the server-side validation, the client-side requires mapping the data class properties to their corresponding page input control, but is simpler as it only requires the property name and the target control instance.<br />
<a href="https://gist.github.com/johnkevincheng/a47b1881c1c60a0b8425c5e284c04907.js">Link to Gist</a>

Shown in the top half is the plain BootStrap-designed row for the <em>FirstName</em> field. This is how a field would typically look like before the client-side validators are added. After the  <em>InitializeClientValidators()</em> function has been called and the <em>FirstName</em> field has been found to contain a validator attribute, then the framework uses the provided implementor class to decorate the <em>input</em> tag with the necessary HTML attributes as required by BootStrap Validations library. The bottom half in the snippet above shows the new attributes added to the <em>input</em> tag with the rules for this field's validations.<br />
<br />

In case the built-in implementor is not exactly right to your needs, or you are using a totally different client-side validation strategies, you may use your own implementor by implementing the <strong>IValidationImplementor</strong> interface. The snippet below shows the contents of the <strong>BootstrapValidationImplementor</strong> class, which you may base your own off of (each function handles the insertion of required attributes depending on their validator types).<br />
<a href="https://gist.github.com/johnkevincheng/db45b45150b1baad6b9ef0e5f7af3ec1.js">Link to Gist</a>

And that sets up both client-side and server-side validations on an ASP.NET page.<br />
<br />

There will be more validator attributes to be added in the future, as well as a few more implementors. If you like what you see and wish to make some suggestion, do leave a comment.
