'use server';

export async function startPRReview(_: any, formData:any) {
    'use server'
        
    if (formData != null){
      const rawFormData = {
        prNumber: formData.get('pr-number-to-review'),
        repoName: formData.get('git-repo-name'),
        gitToken: formData.get('git-personal-token'),
      }
      console.log('Calling HTTP API')
      var response = await fetch('http://localhost:5070/PullRequestReview', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(rawFormData),
        });

        var json = await response.json()
        const responseJson ={
          status: true,
          suggestions: json
      }
      console.log('responseJson', responseJson)
      return responseJson
    }
}