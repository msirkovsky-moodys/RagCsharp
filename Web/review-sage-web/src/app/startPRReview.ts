'use server';

export async function startPRReview(_: any, formData:any) {
    'use server'
        
    if (formData != null){
      const rawFormData = {
        prNumber: formData.get('pr-number-to-review'),
        repoName: formData.get('git-repo-name'),
        gitToken: formData.get('git-personal-token'),
      }

      fetch('localhost:7000/api/startPRReview', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(rawFormData),
      }).then((response) => {
        console.log('TEST:' + response)
      })
  }
    return 'test'
  }